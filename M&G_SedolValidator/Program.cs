using M_G_SedolValidator;

class Program
{
    static void Main(string[] args)
    {
        var tester = new SedolValidatorTester();
        tester.TestSedolValidator(new SedolValidator());
    }
}

public class SedolValidatorTester 
{
    public void TestSedolValidator(ISedolValidator validator)
    {
        // Test scenarios
        TestScenario(validator, null!, false, false, "Input string was not 7-characters long");
        TestScenario(validator, "", false, false, "Input string was not 7-characters long");
        TestScenario(validator, "12", false, false, "Input string was not 7-characters long");
        TestScenario(validator, "123456789", false, false, "Input string was not 7-characters long");
        TestScenario(validator, "1234567", false, false, "Checksum digit does not agree with the rest of the input");
        TestScenario(validator, "0709954", true, false, null!);
        TestScenario(validator, "B0YBKJ7", true, false, null!);
        TestScenario(validator, "9123451", false, true, "Checksum digit does not agree with the rest of the input");
        TestScenario(validator, "9ABCDE8", false, true, "Checksum digit does not agree with the rest of the input");
        TestScenario(validator, "9123_51", false, false, "SEDOL contains invalid characters");
        TestScenario(validator, "VA.CDE8", false, false, "SEDOL contains invalid characters");
        TestScenario(validator, "9123458", true, true, null!);
        TestScenario(validator, "9ABCDE1", true, true, null!);
    }

    private static void TestScenario(ISedolValidator validator, string input, bool isValidSedolExpected, bool isUserDefinedExpected, string validationDetailsExpected)
    {
        var result = validator.ValidateSedol(input);
        bool isValid = result.IsValidSedol == isValidSedolExpected && result.IsUserDefined == isUserDefinedExpected &&
                       (result.ValidationDetails == validationDetailsExpected || (result.ValidationDetails == null && validationDetailsExpected == null));
        Console.WriteLine($"Input: {input}, IsValidSedol: {result.IsValidSedol}, IsUserDefined: {result.IsUserDefined}, ValidationDetails: {result.ValidationDetails}, Test Passed: {isValid}");
    }
}
