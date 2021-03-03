using System;

namespace PayParkingLibrary.EventHandling.Events
{
	// TODO: Create a base class ParkingEventResultBase, and a ParkingEventEnterResult htat inherits from it. 
	//       ParkingEventleaveResult will alsno inherit from ParkingEventResultBase
	public class ParkingEventResult
	{
		public bool Success { get; }

		public string Message { get; }

		public string? RegistrationNumber { get; }

		public DateTime Date { get; }

		public ParkingEventResult(bool success, string message, DateTime date)
			: this (success, message, default, date)
		{
		}

		public ParkingEventResult(bool success, string message, string? registrationNumber, DateTime date)
		{
			Success = success;
			Message = message;
			RegistrationNumber = registrationNumber;
			Date = date;
		}

		public virtual string ConstructOutput()
		{
			if (Success)
			{
				return $"{Message}{Environment.NewLine}" +
					$"\t{RegistrationNumber} parked{Environment.NewLine}" +
					$"\tFees: 10 LEU first hour, 5 LEU subsequent hours.{Environment.NewLine}";
			}
			else
			{
				return Message;
			}
		}
			
	}
}