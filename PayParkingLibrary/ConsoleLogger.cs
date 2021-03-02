using System;

namespace PayParkingLibrary
{
	public class ConsoleLogger
	{
		public void WriteLine(object messageObj)
			=> WriteLine(DateTime.UtcNow, messageObj.ToString());

		public void WriteLine(DateTime timestamp, object messageObj)
			=> Console.WriteLine($"{timestamp}: {messageObj}{Environment.NewLine}");
	}
}
