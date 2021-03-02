using PayParkingLibrary.EventHandling.Events;
using System;
using System.Collections.Generic;

namespace PayParkingLibrary.EventHandling.Handlers
{
	public class ParkingEventHandler : AbstractEventHandler
	{
		private Dictionary<char, IParkingEventHandler> parkingEventHandlers;

		public ParkingEventHandler()
		{
			parkingEventHandlers = new Dictionary<char, IParkingEventHandler>();
		}

		public ParkingEventHandler(IParkingEventHandler[] eventHandlers)
			: this()
		{
			foreach(var handler in eventHandlers)
			{
				RegisterEventHandler(handler);
			}
		}

		public override void RegisterEventHandler(IParkingEventHandler handler)
		{
			parkingEventHandlers.Add(handler.GetOperation(), handler);
		}

		public override ParkingEventResult Handle(ParkingEvent parkingEvent)
		{
			var found = parkingEventHandlers.TryGetValue(parkingEvent.OperationCode, out IParkingEventHandler eventHandler);

			if (!found)
			{
				// No handler is registered for the specific operation. I prefer not validating this in ParkingInputValidator.
				return new ParkingEventResult(false, $"Invalid operation: {parkingEvent.OperationCode}", DateTime.UtcNow); 
			}

			return eventHandler.Handle(parkingEvent);
		}
	}
}
