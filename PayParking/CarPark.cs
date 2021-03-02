using PayParkingLibrary;
using PayParkingLibrary.EventHandling.Events;
using PayParkingLibrary.EventHandling.Handlers;
using PayParkingLibrary.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PayParking
{
	public class CarPark : ICarPark
	{
		private readonly ConsoleLogger logger;
		private readonly ParkingInputValidator parkingInputValidator;
		private readonly ParkingEventHandler parkingEventHandler;

		public Dictionary<string, DateTime> ParkedCars { get; }
		public int Capacity { get; }

		public CarPark()
		{
			ParkedCars = new Dictionary<string, DateTime>();
		}

		public CarPark(int capacity, ConsoleLogger logger, ParkingInputValidator inputValidator, ParkingEventHandler eventHandler) 
			: this()
		{
			this.logger = logger;
			this.parkingInputValidator = inputValidator;
			this.parkingEventHandler = eventHandler;
			this.Capacity = capacity;
		}

		public void HandleInput(string input)
		{
			var validationResult = parkingInputValidator.Validate(input);
			if (!validationResult.IsValid)
			{
				logger.WriteLine(validationResult.Errors.FirstOrDefault());
				return;
			}

			var parkingEvent = ConstructParkingEvent(input);
			var eventHandlingResult = parkingEventHandler.Handle(parkingEvent);
			
			logger.WriteLine(eventHandlingResult.Date, eventHandlingResult.ConstructOutput());
		}

		private ParkingEvent ConstructParkingEvent(string input)
			=> new ParkingEvent(
					operationCode: input[0],
					registrationNumber: input.Substring(1).Trim().ToUpper(),
					ParkedCars,
					Capacity);
	}
}
