using System;

namespace PayParkingLibrary.EventHandling.Events
{
	public class ParkingEventLeaveResult : ParkingEventResult
	{
		public DateTime? CarEntryDate { get; set; }
		public double? Duration { get; }
		public decimal? Fee { get; set; }

		public ParkingEventLeaveResult(bool success, string message, string registrationNumber, DateTime eventDate)
			: this(success, message, registrationNumber, eventDate, default, default, default)
		{
		}

		public ParkingEventLeaveResult(bool success, string message, string registrationNumber, DateTime eventDate, DateTime? carEntryDate, double? duration, decimal? fee)
			: base(success, message, registrationNumber, eventDate)
		{
			CarEntryDate = carEntryDate;
			Duration = duration;
			Fee = fee;
		}

		public override string ConstructOutput()
		{
			if (Success)
			{
				return $"{Message}{Environment.NewLine}" +
					$"\t{RegistrationNumber} left parking.{Environment.NewLine}" +
					$"\tEntry time: {CarEntryDate}{Environment.NewLine}" +
					$"\tDuration: {Math.Round(Duration.GetValueOrDefault(), 2)} h{Environment.NewLine}" +
					$"\tFee: {Fee} LEU";
			}
			else
			{
				return Message;
			}
		}
	}
}
