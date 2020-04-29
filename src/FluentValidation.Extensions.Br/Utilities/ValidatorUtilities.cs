using System.Text.RegularExpressions;

namespace FluentValidation.Extensions.Br.Utilities
{
    public class ValidatorUtilities : IValidatorUtilities
    {
        private readonly Regex _nonDigitsRegex = new Regex(@"\D");

        public string RemoveNonDigits(string content)
            => string.IsNullOrWhiteSpace(content)
                ? content
                : _nonDigitsRegex.Replace(content, "");
    }
}
