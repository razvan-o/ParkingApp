using PayParkingLibrary.EventHandling.Events;
using PayParkingLibrary.Services;
using PayParkingLibrary.Validators;
using System;
using System.Collections.Generic;

namespace PayParkingLibrary.EventHandling.Handlers
{
	public class LeaveParkingEventHandler : IParkingEventHandler
	{
		private const char OperationCode = '-';
		private const int firstHourFee = 10;
		private const int subsequentHourFee = 5;

		public char GetOperation()
			=> OperationCode;

		public ParkingEventResult Handle(ParkingEvent parkingEvent)
		{
			(var validationSuccessful, var validationErrors) = CheckForValidOperation(parkingEvent.RegistrationNumber, parkingEvent.ParkedCars, parkingEvent.Capacity);
			var timestamp = DateTime.UtcNow;

			if (!validationSuccessful)
			{
				return new ParkingEventLeaveResult(validationSuccessful, validationErrors, default, timestamp);
			}

			(var fee, var duration) = CalculateFee(parkingEvent.ParkedCars[parkingEvent.RegistrationNumber]);
			var paymentSucces = PaymentService.HandlePayment(fee);

			if (paymentSucces)
			{
				// lift barrier
				var result = new ParkingEventLeaveResult(
					success: true, 
					message: "Have a good day!",
					parkingEvent.RegistrationNumber,
					timestamp,
					carEntryDate: parkingEvent.ParkedCars[parkingEvent.RegistrationNumber],
					duration,
					fee);

				parkingEvent.ParkedCars.Remove(parkingEvent.RegistrationNumber);
				return result;
			}

			return new ParkingEventLeaveResult(false, $"Payment error, could not charge {fee} LEU fee", parkingEvent.RegistrationNumber, timestamp);
		}

		private (int, double) CalculateFee(DateTime entryTime)
		{
			var modifiedTimeSpent = (DateTime.UtcNow - entryTime).Multiply(1200); // each second will count as 20 min.

			return modifiedTimeSpent.TotalSeconds % 3600 == 0
				? (firstHourFee + ((int)modifiedTimeSpent.TotalHours - 1) * subsequentHourFee, modifiedTimeSpent.TotalHours)
				: (firstHourFee + ((int)modifiedTimeSpent.TotalHours) * subsequentHourFee, modifiedTimeSpent.TotalHours);
		}

		public (bool, string) CheckForValidOperation(string registrationNumber, Dictionary<string, DateTime> parkedCars, int? capacity)
		{
			if (!parkedCars.ContainsKey(registrationNumber))
			{
				return (false, ErrorMessages.RegistrationNumberNotFound);
			}
			if (parkedCars[registrationNumber] >= DateTime.UtcNow)
			{
				return (false, ErrorMessages.InvalidTimestamps);
			}

			return (true, default);
		}
	}
}
