namespace M_G_SedolValidator
{
    internal class SedolValidationResult(string inputString, bool isValidSedol, bool isUserDefined, string validationDetails) : ISedolValidationResult
    {
        public string InputString { get; } = inputString;
        public bool IsValidSedol { get; } = isValidSedol;
        public bool IsUserDefined { get; } = isUserDefined;
        public string ValidationDetails { get; } = validationDetails;
    }
}
