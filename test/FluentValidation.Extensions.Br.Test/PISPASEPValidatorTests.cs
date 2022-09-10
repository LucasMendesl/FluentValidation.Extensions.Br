namespace FluentValidation.Extensions.Br.Test
{
    using Results;
    using Xunit;
    using System.Linq;

    public class PISPASEPValidatorTests
    {
        [Theory]
        [InlineData("05078277508")]
        [InlineData("84114792322")]
        [InlineData("91994330670")]
        [InlineData("45085400980")]
        [InlineData("634.66913.68-9")]
        [InlineData("619.10409.08-2")]
        [Trait("PIS/PASEP", "Valid PIS/PASEP")]
        public void When_PIS_Is_Valid_Then_Validator_Should_Pass(string pis)
        {
            // Arrange
            var validator = new TestExtensionsValidator(x => x.RuleFor(r => r.PISPASEP).IsValidPISPASEP());

            // Act
            var validationResult = validator.Validate(new Person { PISPASEP = pis });

            // Assert
            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }

        [Theory]
        [InlineData("05078277500")]
        [InlineData("84114792321")]
        [InlineData("634.66913.68-5")]
        [InlineData("619.10409.08-6")]
        [Trait("PIS/PASEP", "Invalid PIS/PASEP")]
        public void When_PIS_Is_Invalid_Then_Validator_Should_Fail(string pis)
        {
            // Arrange
            var errorMessage = "The PIS/PASEP is invalid";
            var validator = new TestExtensionsValidator(x => x.RuleFor(r => r.PISPASEP).IsValidPISPASEP().WithMessage(errorMessage));

            // Act
            var validationResult = validator.Validate(new Person { PISPASEP = pis });

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.NotEmpty(validationResult.Errors);
            var returnedErrorMessage = validationResult.Errors.First().ErrorMessage;
            Assert.Equal(errorMessage, returnedErrorMessage);
        }

        [Theory]
        [InlineData("05078277500")]
        [InlineData("84114792321")]
        [InlineData("634.66913.68-5")]
        [InlineData("619.10409.08-6")]
        [Trait("PIS/PASEP", "Invalid PIS/PASEP with Default Error Message")]
        public void When_PIS_Is_Invalid_Then_Validator_Should_Fail_With_Default_ErrorMessage(string pis)
        {
            // Arrange
            var expectedMessage = "O PIS/PASEP é inválido!";
            var validator = new TestExtensionsValidator(x => x.RuleFor(r => r.PISPASEP).IsValidPISPASEP());

            // Act
            var validationResult = validator.Validate(new Person { PISPASEP = pis });

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.NotEmpty(validationResult.Errors);
            var returnedErrorMessage = validationResult.Errors.First().ErrorMessage;
            Assert.Equal(expectedMessage, returnedErrorMessage);
        }

        [Theory]
        [InlineData("050")]
        [InlineData("634.68-5")]
        [InlineData("619.10409.08-6844848")]
        [Trait("PIS/PASEP", "Invalid Length")]
        public void When_PIS_Has_Invalid_Length_Then_Validator_Should_Fail(string pis)
        {
            // Arrange
            var errorMessage = "The PIS/PASEP is invalid";
            var validator = new TestExtensionsValidator(x => x.RuleFor(r => r.PISPASEP).IsValidPISPASEP().WithMessage(errorMessage));

            // Act
            var validationResult = validator.Validate(new Person { PISPASEP = pis });

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.NotEmpty(validationResult.Errors);
            var returnedErrorMessage = validationResult.Errors.First().ErrorMessage;
            Assert.Equal(errorMessage, returnedErrorMessage);
        }

        [Fact]
        [Trait("PIS/PASEP", "Null Value")]
        public void When_PIS_Is_Null_Then_Validator_Should_Fail()
        {
            // Arrange
            var errorMessage = "The PIS/PASEP is invalid";
            var validator = new TestExtensionsValidator(x => x.RuleFor(r => r.PISPASEP).IsValidPISPASEP().WithMessage(errorMessage));

            // Act
            var validationResult = validator.Validate(new Person { PISPASEP = null });

            // Assert
            Assert.False(validationResult.IsValid);
            Assert.NotEmpty(validationResult.Errors);
            var returnedErrorMessage = validationResult.Errors.First().ErrorMessage;
            Assert.Equal(errorMessage, returnedErrorMessage);
        }
    }
}
