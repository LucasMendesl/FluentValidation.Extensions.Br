using FluentValidation.Extensions.Br.Utilities;
using Xunit;

namespace FluentValidation.Extensions.Br.Test.Utilities
{
    public class ValidatorUtilitiesTests
    {
        private readonly IValidatorUtilities _utilities;

        public ValidatorUtilitiesTests()
        {
            _utilities = new ValidatorUtilities();
        }

        [Theory]
        [InlineData("1")]
        [InlineData("1234567890")]
        [InlineData("09876543210")]
        [Trait("ValidatorUtilities", "Only Digits")]
        public void When_Only_Digits_Should_Return_The_Same_Value(string value)
        {
            // Arrange / Act
            var valueReturned = _utilities.RemoveNonDigits(value);

            // Assert
            Assert.Equal(value, valueReturned);
        }

        [Theory]
        [InlineData("123.456-91", "12345691")]
        [InlineData("123,456#", "123456")]
        [InlineData("1|2|3", "123")]
        [InlineData("#.*@", "")]
        [Trait("ValidatorUtilities", "Some Non Digits")]
        public void When_Some_Non_Digits_Should_Return_Only_Digits(string value, string expected)
        {
            // Arrange / Act
            var valueReturned = _utilities.RemoveNonDigits(value);

            // Assert
            Assert.Equal(expected, valueReturned);
        }

        [Fact]
        [Trait("ValidatorUtilities", "Null Value")]
        public void When_Null_Should_Return_Null()
        {
            // Arrange / Act
            var valueReturned = _utilities.RemoveNonDigits(null);

            // Assert
            Assert.Equal(null, valueReturned);
        }
    }
}
