using PayParkingLibrary;
using System;
using System.Linq;

namespace PayParking
{
	public class CarParkGUIDecorator : ICarPark
	{
		private readonly CarPark carPark;
		private readonly ConsoleLogger consoleLogger;

		public CarParkGUIDecorator(CarPark carPark, ConsoleLogger consoleLogger)
		{
			this.carPark = carPark;
			this.consoleLogger = consoleLogger;
		}

		public void HandleInput(string input)
		{
			if (input == "status")
			{
				consoleLogger.WriteLine($"{carPark.Capacity - carPark.ParkedCars.Count}/{carPark.Capacity} spots available.");
			}
			else if (input == "list")
			{
				var carList = String.Join(",", carPark.ParkedCars.Keys);
				consoleLogger.WriteLine($"Parked cars: {(carList == string.Empty ? "-" : carList)}.");
			}
			else if(input != String.Empty) // my fluent validator won't handle this case for some reason
			{
				carPark.HandleInput(input);
			}
		}
	}
}
