namespace FluentValidation
{
    using FluentValidation.Extensions.Br.Utilities;
    using FluentValidation.Extensions.Br.Validators;
    using Validators;

    /// <summary>
    /// A partial class with brazilian extension methods for validations
    /// </summary>
    public static partial class DefaultValidatorExtensions
    {
        /// <summary>
        /// Defines a 'CNPJ' validator on the current rule builder.
        /// Validation will fail if the property is null, an empty or the value is a invalid CNPJ         
        /// </summary>
        /// <typeparam name="T">Type of object being validated</typeparam>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
        /// <returns>a rule builder with cnpj validation included</returns>
        public static IRuleBuilderOptions<T, string> IsValidCNPJ<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CNPJValidator());
        }

        /// <summary>
        /// Defines a 'CPF' validator on the current rule builder.
        /// Validation will fail if the property is null, an empty or the value is a invalid CPF         
        /// </summary>
        /// <typeparam name="T">Type of object being validated</typeparam>
        /// <param name="ruleBuilder">The rule builder on which the validator should be defined</param>
        /// <returns>a rule builder with cnpj validation included</returns>
        public static IRuleBuilderOptions<T, string> IsValidCPF<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.SetValidator(new CPFValidator());
        }

        public static IRuleBuilderOptions<T, string> IsValidPIS<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var utilities = new ValidatorUtilities();
            return ruleBuilder.SetValidator(new PISValidator(utilities));
        }
    }
}
