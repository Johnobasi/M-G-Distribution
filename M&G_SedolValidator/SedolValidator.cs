namespace M_G_SedolValidator
{
    public class SedolValidator : ISedolValidator
    {
        public SedolValidator() { }
        public ISedolValidationResult ValidateSedol(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length != 7)
            {
                return new SedolValidationResult(input, false, false, "Input string was not 7-characters long");
            }

            if (ContainsInvalidCharacters(input))
            {
                return new SedolValidationResult(input, false, false, "SEDOL contains invalid characters");
            }

            if (!IsUserDefinedSedol(input))
            {
                if (!IsValidChecksum(input))
                {
                    return new SedolValidationResult(input, false, false, "Checksum digit does not agree with the rest of the input");
                }

                return new SedolValidationResult(input, true, false, null!);
            }

            if (!IsValidChecksum(input))
            {
                return new SedolValidationResult(input, false, true, "Checksum digit does not agree with the rest of the input");
            }

            return new SedolValidationResult(input, true, true, null!);
        }


        #region Private Methods

        /// <summary>
        ///  his method is used to validate the checksum of the SEDOL
        /// </summary>
        /// <param name="input">Description of the parameter.</param>
        /// <returns>Method returns a boolean.</returns>
        private static bool IsValidChecksum(string input)
        {
            int sum = 0;
            int[] weights = { 1, 3, 1, 7, 3, 9 };

            for (int i = 0; i < 6; i++)
            {
                int value = GetCharValue(input[i]);
                sum += value * weights[i];
            }

            int checkDigit = (10 - (sum % 10)) % 10;
            return checkDigit == int.Parse(input[6].ToString());
        }


        /// <summary>
        ///  This method is used to get the integer value of a character
        /// </summary>
        /// <param name="c">Description of the parameter.</param>
        /// <returns>Method return value of a char.</returns>
        private static int GetCharValue(char c)
        {
            if (char.IsDigit(c))
            {
                return int.Parse(c.ToString());
            }
            else
            {
                return char.ToUpper(c) - 'A' + 11;
            }
        }

        /// <summary>
        /// This method is used to check if the SEDOL is user defined
        /// </summary>
        /// <param name="input">Description of the parameter.</param>
        /// <returns>Method return a boolean.</returns>
        private static bool IsUserDefinedSedol(string input)
        {
            return input[0] == '9';
        }

        /// <summary>
        /// This method is used to check if the SEDOL contains invalid characters
        /// </summary>
        /// <param name="input">Description of the parameter.</param>
        /// <returns>Method return a boolean.</returns>
        private static bool ContainsInvalidCharacters(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}
