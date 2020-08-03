namespace FluentValidation.Validators
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;


    /// <summary>
    /// Base class for brazilian person´s document validation (CPF/CNPJ).
    /// </summary>
    public abstract class GenericPersonValidator : PropertyValidator, IBrazilianPropertyValidator
    {
        private readonly int validLength;

        protected abstract int[] FirstMultiplierCollection { get; }
        protected abstract int[] SecondMultiplierCollection { get; }

        protected GenericPersonValidator(int validLength, string errorMessage)
            : base(errorMessage)
        {
            this.validLength = validLength;
        }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var value = context.PropertyValue as string ?? string.Empty;
            value = Regex.Replace(value, "[^a-zA-Z0-9]", "");

            if (IsValidLength(value) || 
                AllDigitsAreEqual(value) || 
                context.PropertyValue == null) return false;

            var cpf = value.Select(x => (int)Char.GetNumericValue(x)).ToArray();
            var digits = GetDigits(cpf);

            return value.EndsWith(digits);
        }

        static bool AllDigitsAreEqual (string value) => value.All(x => x == value.FirstOrDefault());

        bool IsValidLength (string value) => !string.IsNullOrWhiteSpace(value) && value.Length != validLength;

        string GetDigits(int[] cpf)
        {
            var first = CalculateValue(FirstMultiplierCollection, cpf);
            var second = CalculateValue(SecondMultiplierCollection, cpf);

            return $"{CalculateDigit(first)}{CalculateDigit(second)}";
        }

        static int CalculateValue(int[] weight, int[] numbers)
        {
            var sum = 0;
            for (int i = 0; i < weight.Length; i++) sum += weight[i] * numbers[i];
            return sum;
        }

        static int CalculateDigit(int sum)
        {
            int modResult = (sum % 11);
            return modResult < 2 ? 0 : 11 - modResult;
        }
    }
}
