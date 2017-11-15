namespace FluentValidation.Extensions.Br.Test
{
    using Results;
    using Xunit;
    using System.Linq;

    public class CNPJValidatorTests
    {
        [Fact]
        public void When_CNPJ_Is_Valid_Then_The_Validator_Should_Pass ()
        {
            TestExtensionsValidator validator = new TestExtensionsValidator(x => x.RuleFor(r => r.CNPJ).IsValidCNPJ());
            ValidationResult result = validator.Validate(new Person { CNPJ = "11.434.325/0001-07" });

            Assert.True(result.IsValid);
        }

        [Fact]
        public void When_CNPJ_Is_Invalid_Then_Set_Custom_Message_And_Validator_Should_Fail()
        {
            const string customMessage = "Custom Message";

            TestExtensionsValidator validator = new TestExtensionsValidator(x => x.RuleFor(r => r.CNPJ).IsValidCNPJ().WithMessage(customMessage));
            ValidationResult result = validator.Validate(new Person { CNPJ = "00.000.000/0000-00" });

            string errorMessage = result.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty;

            Assert.False(result.IsValid);
            Assert.Equal(customMessage, errorMessage);
        }


        [Theory]
        [InlineData("14.442.344/1210-57")] 
        [InlineData("11.434.3215/00123-57")] 
        public void When_CNPJ_Is_Invalid_Then_The_Validator_Should_Fail (string cnpj)
        {
            TestExtensionsValidator validator = new TestExtensionsValidator(x => x.RuleFor(r => r.CNPJ).IsValidCNPJ());
            ValidationResult result = validator.Validate(new Person { CNPJ = cnpj });

            Assert.False(result.IsValid);
        }
    }
}
