using System.Linq;

namespace FluentValidation.Validators
{
    /// <summary>
    /// Ensures that the property value is a valid UF.
    /// </summary>
    public class UFValidator<T, TProperty> : PropertyValidator<T, TProperty>, IBrazilianPropertyValidator
    {
        private static string[] UFCollection => new string[27] { "AC", "AL", "AM", "AP", "BA", "CE", "DF", "ES", "GO", "MA", "MG", "MS", "MT", "PA", "PB", "PE", "PI", "PR", "RJ", "RN", "RO", "RR", "RS", "SC", "SE", "SP", "TO" };

        public override string Name => "UFValidator";

        protected override string GetDefaultMessageTemplate(string errorCode) => "O UF é inválido!";

        public override bool IsValid(ValidationContext<T> context, TProperty value)
        {
            var uf = value as string;

            if (string.IsNullOrEmpty(uf))
                return false;

            if (uf.Length != 2)
                return false;

            return UFCollection.Contains(uf);
        }
    }
}
