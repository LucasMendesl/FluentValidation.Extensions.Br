namespace FluentValidation.Validators
{
    /// <summary>
    /// Ensures that the property value is a valid CPF number.
    /// </summary>
    public class CPFValidator : GenericPersonValidator
    {
        internal CPFValidator(int validLength, string errorMessage) 
            : base(validLength, errorMessage)
        { }

        public CPFValidator(string errorMessage)
            : this(11, errorMessage)
        { }

        public CPFValidator()
            : this("O CPF é inválido!")
        { }

        protected override int[] FirstMultiplierCollection => new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        protected override int[] SecondMultiplierCollection => new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
    }
}
