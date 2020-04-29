using FluentValidation.Extensions.Br.Utilities;
using FluentValidation.Validators;
using System.Linq;

namespace FluentValidation.Extensions.Br.Validators
{
    public class PISValidator : PropertyValidator, IBrazilianPropertyValidator
    {
        private readonly IValidatorUtilities _validatorUtilities;

        public PISValidator(string errorMessage)
            : base(errorMessage)
        { }

        public PISValidator(IValidatorUtilities validatorUtilities)
            : this("O PIS é ínválido!")
        {
            _validatorUtilities = validatorUtilities;
        }

        protected virtual int[] FirstMultiplierCollection => new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var propValue = context.PropertyValue as string;

            var value = _validatorUtilities.RemoveNonDigits(propValue);

            if (value == null || value.Length != 11)
                return false;

            return Calculate(value);
        }

        private bool Calculate(string value)
        {
            int[] digits = value.Select(x => (int)char.GetNumericValue(x)).ToArray();
            int sum = 0;
            for (int index = 0, limit = value.Length - 1; index < limit; index++)
                sum += digits[index] * FirstMultiplierCollection[index];

            var rest = sum % 11;
            var checkDigit = rest > 1 ? 11 - rest : 0;

            return digits[10] == checkDigit;
        }
    }
}
