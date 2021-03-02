using System;

namespace PayParkingLibrary.Services
{
	public static class PaymentService
	{
		public static bool HandlePayment(int fee)
		{
			// Capture user card details
			// Attempt payment - with retry policies

			// will assume payment goes through.
			return new Random().Next(0,99) % 2 == 0;
		}
	}
}
