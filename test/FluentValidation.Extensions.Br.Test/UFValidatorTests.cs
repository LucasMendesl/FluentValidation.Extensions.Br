namespace FluentValidation.Extensions.Br.Test
{
    using Results;
    using Xunit;
    using System.Linq;

    public class UFValidatorTests
    {
        [Fact]
        public void When_UF_Is_Valid_Then_The_Validator_Should_Pass()
        {
            TestExtensionsValidator validator = new TestExtensionsValidator(x => x.RuleFor(r => r.UF).IsValidUF());
            ValidationResult result = validator.Validate(new Person { UF = "RS" });

            Assert.True(result.IsValid);
        }

        [Fact]
        public void When_UF_Is_Invalid_Then_Set_Custom_Message_And_Validator_Should_Fail()
        {
            const string customMessage = "Custom Message";

            TestExtensionsValidator validator = new TestExtensionsValidator(x => x.RuleFor(r => r.UF).IsValidUF().WithMessage(customMessage));
            ValidationResult result = validator.Validate(new Person { UF = "SS" });

            string errorMessage = result.Errors.FirstOrDefault()?.ErrorMessage ?? string.Empty;

            Assert.False(result.IsValid);
            Assert.Equal(customMessage, errorMessage);
        }


        [Theory]     
        [InlineData("RS ")]
        [InlineData(" RS")]
        [InlineData("R S")]
        [InlineData(" RS ")]
        [InlineData("Rs")]
        [InlineData("rS")]        
        [InlineData("rs")]
        public void When_UF_Is_Invalid_Then_The_Validator_Should_Fail(string uf)
        {
            TestExtensionsValidator validator = new TestExtensionsValidator(x => x.RuleFor(r => r.UF).IsValidUF());
            ValidationResult result = validator.Validate(new Person { UF = uf });

            Assert.False(result.IsValid);
            Assert.Equal("O UF é inválido!", result.Errors.First().ErrorMessage);
        }
    }
}
