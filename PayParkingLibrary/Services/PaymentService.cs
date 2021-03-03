namespace PayParkingLibrary.Services
{
	public class PaymentService
	{
		public virtual bool HandlePayment(int fee)
		{
			// Capture user card details
			// Attempt payment - with retry policies

			return fee % 2 == 0;
		}
	}
}
