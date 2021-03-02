using PayParkingLibrary;
using PayParkingLibrary.EventHandling.Handlers;
using PayParkingLibrary.Validators;
using System;

namespace PayParking
{
	class Program
	{
		static void Main(string[] args)
		{
			var capacity = 3;
			var consoleLogger = new ConsoleLogger();
			var parkingInputValidator = new ParkingInputValidator();
			var parkingEventHandler = new ParkingEventHandler();
			parkingEventHandler.RegisterEventHandler(new EnterParkingEventHandler());
			parkingEventHandler.RegisterEventHandler(new LeaveParkingEventHandler());

			var carPark = new CarPark(capacity, consoleLogger, parkingInputValidator, parkingEventHandler);
			var carParkGUI = new CarParkGUIDecorator(carPark, consoleLogger);

			Console.WriteLine($"Type \"status\" to see available spots.{Environment.NewLine}" +
				$"Type '+' OR '-' followed by registration number to enter/ leave parking lot.{Environment.NewLine}" +
				$"Every second spent parked counts as 20 minutes.{Environment.NewLine}");
			while (true)
			{
				string parkingInput = Console.In.ReadLine();
				carParkGUI.HandleInput(parkingInput);
			}
		}
	}
}
