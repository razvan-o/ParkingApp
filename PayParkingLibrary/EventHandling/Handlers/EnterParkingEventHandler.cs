using PayParkingLibrary.EventHandling.Events;
using PayParkingLibrary.Validators;
using System;
using System.Collections.Generic;

namespace PayParkingLibrary.EventHandling.Handlers
{
	public class EnterParkingEventHandler : IParkingEventHandler
	{
		private const char OperationCode = '+';

		public char GetOperation()
			=> OperationCode;

		public ParkingEventResult Handle(ParkingEvent parkingEvent)
		{
			(var validationSuccessful, var validationErrors) = CheckForValidOperation(parkingEvent.RegistrationNumber, parkingEvent.ParkedCars, parkingEvent.Capacity);
			var timestamp = DateTime.UtcNow;

			if (!validationSuccessful)
			{
				return new ParkingEventResult(validationSuccessful, validationErrors, timestamp);
			}

			parkingEvent.ParkedCars.Add(parkingEvent.RegistrationNumber, timestamp);

			// lift barrier

			return new ParkingEventResult(true, "Welcome!", parkingEvent.RegistrationNumber, timestamp);
		}

		public (bool, string) CheckForValidOperation(string registrationNumber, Dictionary<string, DateTime> parkedCars, int? capacity)
		{
			if (parkedCars.ContainsKey(registrationNumber))
			{
				return (false, ErrorMessages.DuplicateRegistrationNumber);
			}
			else if (parkedCars.Keys.Count >= capacity.Value)
			{
				return (false, ErrorMessages.NoAvailableSpots);
			}

			return (true, default);
		}
	}
}
