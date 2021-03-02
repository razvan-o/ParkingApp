using FluentValidation;

namespace PayParkingLibrary.Validators
{
	public class ParkingInputValidator : AbstractValidator<string>
	{
		private string carRegistrationNumberRegex = @"^[A-Z]{1,2}[0-9]{1,3}[A-Z]{3}$";
		private string operationSymbols = "~!@#$%^&*_+=-";

		public ParkingInputValidator()
		{
			RuleFor(x => x)
				.Cascade(CascadeMode.Stop)
				.NotNull()
				.WithMessage("Input can not be null");

			RuleFor(x => x)
				.Cascade(CascadeMode.Stop)
				.NotEmpty()
				.WithMessage("Input can not be empty");

			RuleFor(x => x.Trim()[0])
				.Must(IsOperationSymbol)
				.WithMessage("The first character in your input must be an operation symbol.");

			RuleFor(x => x.Substring(1).Trim().ToUpper())
				.Matches(carRegistrationNumberRegex)
				.WithMessage("Car registration number must respect {Area}{Number}{Code} format. e.g.: B101COD");
		}

		private bool IsOperationSymbol(char arg)
			=> operationSymbols.Contains(arg);
	}
}
