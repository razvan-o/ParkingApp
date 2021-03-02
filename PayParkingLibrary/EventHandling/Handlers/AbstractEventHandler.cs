using PayParkingLibrary.EventHandling.Events;
using System;
using System.Collections.Generic;

namespace PayParkingLibrary.EventHandling.Handlers
{
	public abstract class AbstractEventHandler
	{
		public abstract void RegisterEventHandler(IParkingEventHandler handler);

		public abstract ParkingEventResult Handle(ParkingEvent parkingEvent);
	}
}
