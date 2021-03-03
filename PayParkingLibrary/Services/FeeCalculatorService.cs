using System;

namespace PayParkingLibrary.Services
{
	public class FeeCalculatorService
	{
		private const int firstHourFee = 10;
		private const int subsequentHourFee = 5;

		public (int, double) CalculateFee(DateTime entryTime, DateTime exitTime)
		{
			if (entryTime > exitTime)
			{
				// inform system administrator
				return (0,0); // I choose to let the person leave the parking lot
			}

			// each second will count as 20 min.
			var timeSpentSeconds = Math.Round((exitTime - entryTime).TotalSeconds);
			var modifiedTimeSpent = timeSpentSeconds * 1200; 

			var fullHours = (int)modifiedTimeSpent / 3600;
			return modifiedTimeSpent % 3600 == 0
				? (firstHourFee + (fullHours - 1) * subsequentHourFee, Math.Round(modifiedTimeSpent / 3600.00, 2))
				: (firstHourFee + (fullHours) * subsequentHourFee, Math.Round(modifiedTimeSpent / 3600.00, 2));
		}
	}
}
