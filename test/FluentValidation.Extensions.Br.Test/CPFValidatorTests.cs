namespace FluentValidation.Extensions.Br.Test
{
    using Results;
    using Xunit;
    using System.Linq;

    public class CPFValidatorTests
    {
        [Fact]
        public void When_CPF_Is_Valid_Then_The_Validator_Should_Pass()
        {
            TestExtensionsValidator validator = new TestExtensionsValidator(x => x.RuleFor(r => r.CPF).IsValidCPF());
            ValidationResult result = validator.Validate(new Person { CPF = "822.420.106-62" });

            Assert.True(result.IsValid);
        }

        [Fact]
        public void When_CPF_Is_Invalid_Then_Set_Custom_Message_And_Validator_Should_Fail()
        {
            const string customMessage = "Custom Message";

            TestExtensionsValidator validator = new TestExtensionsValidator(x => x.RuleFor(r => r.CPF).IsValidCPF().WithMessage(customMessage));
            ValidationResult result = validator.Validate(new Person { CPF = "000.000.000-00" });

            string errorMessage = result.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty;

            Assert.False(result.IsValid);
            Assert.Equal(customMessage, errorMessage);
        }


        [Theory]
        [InlineData("144.442.344-57")]
        [InlineData("543.434.321-76")]
        [InlineData("A822.420.106-62")]
        [InlineData("822.420.106-62a")]
        public void When_CPF_Is_Invalid_Then_The_Validator_Should_Fail(string cpf)
        {
            TestExtensionsValidator validator = new TestExtensionsValidator(x => x.RuleFor(r => r.CPF).IsValidCPF());
            ValidationResult result = validator.Validate(new Person { CPF = cpf });

            Assert.False(result.IsValid);
            Assert.Equal(result.Errors.First().ErrorMessage, "O CPF é inválido!");
        }

    }
}
