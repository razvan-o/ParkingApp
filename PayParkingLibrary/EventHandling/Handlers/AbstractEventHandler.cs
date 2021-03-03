using PayParkingLibrary.EventHandling.Events;

namespace PayParkingLibrary.EventHandling.Handlers
{
	public abstract class AbstractEventHandler
	{
		public abstract void RegisterEventHandler(IParkingEventHandler handler);

		public abstract ParkingEventResult Handle(ParkingEvent parkingEvent);
	}
}
