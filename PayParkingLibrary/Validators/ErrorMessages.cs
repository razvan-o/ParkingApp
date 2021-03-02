namespace PayParkingLibrary.Validators
{
	public static class ErrorMessages
	{
		private static readonly string AdminContact = "Please seek parking adinistrator for assistance. Mobile: 0711 222 333";

		// Possible issue with camera reader; can implement sending notification to parking administrator
		public static readonly string DuplicateRegistrationNumber = $"Car with same registration number already in parking lot. {AdminContact}";

		// Possible issue with camera reader; can implement sending notification to parking administrator
		public static readonly string RegistrationNumberNotFound = $"Car registration number is not recognized. {AdminContact}";

		// Software issue; can implement sending notification to parking administrator
		public static readonly string InvalidTimestamps = $"We are facing some internal issues. {AdminContact}";

		// Can implement flow to collect client data and notify him when a parking spot becomes available
		public static readonly string NoAvailableSpots = "There are no spots available";
	}
}
