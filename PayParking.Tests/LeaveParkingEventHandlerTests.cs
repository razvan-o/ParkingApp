using Moq;
using PayParkingLibrary.EventHandling.Events;
using PayParkingLibrary.EventHandling.Handlers;
using PayParkingLibrary.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace PayParking.Tests
{
	public class LeaveParkingEventHandlerTests
	{
		private readonly DateTime entryTime;
		private LeaveParkingEventHandler leaveParkingEventHandler;
		private List<string> registrationNumbers = new List<string> { "B11ABC", "B12DEF", "B13GHI" };
		private Dictionary<string, DateTime> parkedCars;
		private Mock<PaymentService> paymentService;

		public LeaveParkingEventHandlerTests()
		{
			paymentService = new Mock<PaymentService>();
			paymentService.Setup(x => x.HandlePayment(It.IsAny<int>())).Returns(true);

			leaveParkingEventHandler = new LeaveParkingEventHandler(new FeeCalculatorService(), paymentService.Object);

			parkedCars = new Dictionary<string, DateTime>();
			entryTime = DateTime.UtcNow.AddSeconds(-3);
			foreach (var regNum in registrationNumbers)
			{
				parkedCars.Add(regNum, entryTime);
			}
		}


		[Fact]
		public void ExistingRegistrationNumber_LeavesParking_ShouldSucced()
		{
			var parkingEvent = new ParkingEvent(leaveParkingEventHandler.GetOperation(), registrationNumbers[0], parkedCars, 3);
			var result = leaveParkingEventHandler.Handle(parkingEvent);

			Assert.Equal(true, result.Success);
			Assert.Equal(parkingEvent.RegistrationNumber, result.RegistrationNumber);
		}

		[Fact]
		public void NewRegistrationNumber_LeavesParking_ShouldFail()
		{
			var newRegistrationNumber = "X00ASD";
			var parkingEvent = new ParkingEvent(leaveParkingEventHandler.GetOperation(), newRegistrationNumber, parkedCars, 3);
			var result = leaveParkingEventHandler.Handle(parkingEvent);

			Assert.Equal(false, result.Success);
			Assert.Equal(default, result.RegistrationNumber);
		}
	}
}
