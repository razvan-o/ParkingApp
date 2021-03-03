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
		private readonly FeeCalculatorService feeCalculatorService;
		private readonly PaymentService paymentService;

		public LeaveParkingEventHandler(FeeCalculatorService feeCalculatorService, PaymentService paymentService)
		{
			this.feeCalculatorService = feeCalculatorService;
			this.paymentService = paymentService;
		}

		public char GetOperation() => OperationCode;

		public ParkingEventResult Handle(ParkingEvent parkingEvent)
		{
			(var validationSuccessful, var validationErrors) = CheckForValidOperation(parkingEvent.RegistrationNumber, parkingEvent.ParkedCars, parkingEvent.Capacity);
			var timestamp = DateTime.UtcNow;

			if (!validationSuccessful)
			{
				return new ParkingEventLeaveResult(validationSuccessful, validationErrors, default, timestamp);
			}

			(var fee, var duration) = feeCalculatorService.CalculateFee(parkingEvent.ParkedCars[parkingEvent.RegistrationNumber], timestamp);
			var paymentSucces = paymentService.HandlePayment(fee);

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
