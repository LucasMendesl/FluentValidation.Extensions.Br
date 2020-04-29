using System;

namespace FluentValidation.Extensions.Br.Test
{
    public class TestExtensionsValidator : InlineValidator<Person>
    {
        public TestExtensionsValidator()
        { }

        public TestExtensionsValidator(params Action<TestExtensionsValidator>[] actionList)
        {
            foreach (var action in actionList) action.Invoke(this);
        }
    }
}
