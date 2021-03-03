using PayParkingLibrary.Services;
using System;
using Xunit;

namespace PayParking.Tests
{
	public class FeeCalculatorServiceTests
	{
		private FeeCalculatorService feeCalculatorService;

		public FeeCalculatorServiceTests()
		{
			this.feeCalculatorService = new FeeCalculatorService();
		}

		// We are modifying the duration inside FeeCalculatorService; each second will count as 20 min.

		[Fact]
		public void DurationNegative_ShouldCharge0()
		{
			var exitTime = DateTime.UtcNow;
			var entryTime = DateTime.UtcNow.AddSeconds(1);
			var result = feeCalculatorService.CalculateFee(entryTime, exitTime);

			Assert.Equal((0, 0), result);
		}

		[Fact]
		public void Duration20m_ShouldCharge10()
		{
			var exitTime = DateTime.UtcNow;
			var entryTime = DateTime.UtcNow.AddSeconds(-1);
			var result = feeCalculatorService.CalculateFee(entryTime, exitTime);

			Assert.Equal((10, 0.33), result);
		}

		[Fact]
		public void Duration1h_ShouldCharge10()
		{
			var exitTime = DateTime.UtcNow;
			var entryTime = DateTime.UtcNow.AddSeconds(-3);
			var result = feeCalculatorService.CalculateFee(entryTime, exitTime);

			Assert.Equal((10, 1), result);
		}

		[Fact]
		public void Duration1h20m_ShouldCharge15()
		{
			var exitTime = DateTime.UtcNow;
			var entryTime = DateTime.UtcNow.AddSeconds(-4);
			var result = feeCalculatorService.CalculateFee(entryTime, exitTime);

			Assert.Equal((15, 1.33), result);
		}

		[Fact]
		public void Duration2h_ShouldCharge15()
		{
			var exitTime = DateTime.UtcNow;
			var entryTime = DateTime.UtcNow.AddSeconds(-6);
			var result = feeCalculatorService.CalculateFee(entryTime, exitTime);

			Assert.Equal((15, 2), result);
		}

		[Fact]
		public void Duration10h20m_ShouldCharge15()
		{
			var exitTime = DateTime.UtcNow;
			var entryTime = DateTime.UtcNow.AddSeconds(-31);
			var result = feeCalculatorService.CalculateFee(entryTime, exitTime);

			Assert.Equal((60, 10.33), result);
		}
	}
}
