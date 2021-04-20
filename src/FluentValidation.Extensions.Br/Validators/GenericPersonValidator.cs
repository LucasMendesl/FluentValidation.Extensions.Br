namespace FluentValidation.Validators
{
    using System;
    using System.Linq;
    using System.Text.RegularExpressions;


    /// <summary>
    /// Base class for brazilian person´s document validation (CPF/CNPJ).
    /// </summary>
    public abstract class GenericPersonValidator<T, TProperty> : PropertyValidator<T, TProperty>, IBrazilianPropertyValidator
    {
        private readonly int validLength;
        private readonly string errorMessage;

        protected abstract int[] FirstMultiplierCollection { get; }
        protected abstract int[] SecondMultiplierCollection { get; }

        protected GenericPersonValidator(int validLength, string errorMessage)
        {
            this.validLength = validLength;
            this.errorMessage = errorMessage;
        }

        protected override string GetDefaultMessageTemplate(string errorCode)
        {
            return string.IsNullOrWhiteSpace(errorMessage) ? base.GetDefaultMessageTemplate(errorCode) : errorMessage;
        }

        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            var val = value as string ?? string.Empty;
            val = Regex.Replace(val, "[^a-zA-Z0-9]", "");

            if (IsValidLength(val) || 
                AllDigitsAreEqual(val) || 
                value == null) return false;

            var cpf = val.Select(x => (int)char.GetNumericValue(x)).ToArray();
            var digits = GetDigits(cpf);

            return val.EndsWith(digits);
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
