using System;
using System.Collections.Generic;

namespace PayParkingLibrary.EventHandling.Events
{
	public class ParkingEvent
	{
		public char OperationCode { get; }

		public string RegistrationNumber { get; }

		public Dictionary<string, DateTime> ParkedCars { get; }

		public int Capacity { get; }

		public ParkingEvent(char operationCode, string registrationNumber, Dictionary<string, DateTime> parkedCars, int capacity)
		{
			OperationCode = operationCode;
			RegistrationNumber = registrationNumber;
			ParkedCars = parkedCars;
			Capacity = capacity;
		}
	}
}
