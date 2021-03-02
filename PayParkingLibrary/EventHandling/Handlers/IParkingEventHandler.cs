using PayParkingLibrary.EventHandling.Events;
using System;
using System.Collections.Generic;

namespace PayParkingLibrary.EventHandling.Handlers
{
	public interface IParkingEventHandler
	{
		public char GetOperation();

		public ParkingEventResult Handle(ParkingEvent parkingEvent);

		public (bool, string) CheckForValidOperation(string registrationNumber, Dictionary<string, DateTime> parkedCars, int? capacity);
	}
}
