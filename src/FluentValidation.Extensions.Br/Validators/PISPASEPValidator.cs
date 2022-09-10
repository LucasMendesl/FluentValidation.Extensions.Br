namespace FluentValidation.Validators
{
    using System.Linq;
    using System.Text.RegularExpressions;

    public class PISPASEPValidator<T, TProperty> : PropertyValidator<T, TProperty>, IBrazilianPropertyValidator
    {
        private static readonly Regex _nonDigitsRegex = new Regex(@"\D");
        private static int[] MultiplierCollection => new int[10] { 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        public override string Name => "PISPASEPValidator";

        protected override string GetDefaultMessageTemplate(string errorCode) => "O PIS/PASEP é inválido!";

        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            var pispasep = value as string;

            if (string.IsNullOrEmpty(pispasep))
                return false;

            var pispasepDigits = _nonDigitsRegex.Replace(pispasep, "");

            if (pispasepDigits == null || pispasepDigits.Length != 11)
                return false;

            int[] digits = pispasepDigits.Select(x => (int)char.GetNumericValue(x)).ToArray();
            int sum = 0;
            for (int index = 0, limit = pispasepDigits.Length - 1; index < limit; index++)
                sum += digits[index] * MultiplierCollection[index];

            var rest = sum % 11;
            var checkDigit = rest > 1 ? 11 - rest : 0;

            return digits[10] == checkDigit;
        }
    }
}
