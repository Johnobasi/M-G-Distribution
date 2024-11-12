using FluentAssertions;
using M_G_SedolValidator;

namespace M_G_SedolValidator_Tests
{
    public class SedolValidatorTests
    {
        [Fact]
        public void SedolValidator_NullInput_ReturnsFalse()
        {
            // Arrange
            var validator = new SedolValidator();

            // Act
            var result = validator.ValidateSedol(null!);

            // Assert
            result.IsValidSedol.Should().BeFalse();
            result.IsUserDefined.Should().BeFalse();
            result.ValidationDetails.Should().Be("Input string was not 7-characters long");
        }

        [Fact]
        public void SedolValidator_ShortInput_ReturnsFalse()
        {
            // Arrange
            var validator = new SedolValidator();

            // Act
            var result = validator.ValidateSedol("123");

            // Assert
            result.IsValidSedol.Should().BeFalse();
            result.IsUserDefined.Should().BeFalse();
            result.ValidationDetails.Should().Be("Input string was not 7-characters long");
        }

        [Fact]
        public void SedolValidator_InvalidSedol_ReturnsFalse()
        {
            // Arrange
            var validator = new SedolValidator();

            // Act
            var result = validator.ValidateSedol("1234567");

            // Assert
            result.IsValidSedol.Should().BeFalse();
            result.IsUserDefined.Should().BeFalse();
            result.ValidationDetails.Should().Be("Checksum digit does not agree with the rest of the input");
        }

        [Fact]
        public void SedolValidator_InvalidUserDefinedSedol_ReturnsFalse()
        {
            // Arrange
            var validator = new SedolValidator();

            // Act
            var result = validator.ValidateSedol("9123451");

            // Assert
            result.IsValidSedol.Should().BeFalse();
            result.IsUserDefined.Should().BeTrue();
            result.ValidationDetails.Should().Be("Checksum digit does not agree with the rest of the input");
        }

        [Fact]
        public void SedolValidator_ValidSedol_ReturnsTrue()
        {
            // Arrange
            var validator = new SedolValidator();

            // Act
            var result = validator.ValidateSedol("0709954");

            // Assert
            result.IsValidSedol.Should().BeTrue();
            result.IsUserDefined.Should().BeFalse();
            result.ValidationDetails.Should().BeNull();
        }

        [Fact]
        public void SedolValidator_ValidUserDefinedSedol_ReturnsTrue()
        {
            // Arrange
            var validator = new SedolValidator();

            // Act
            var result = validator.ValidateSedol("9123458");

            // Assert
            result.IsValidSedol.Should().BeTrue();
            result.IsUserDefined.Should().BeTrue();
            result.ValidationDetails.Should().BeNull();
        }

        [Fact]
        public void SedolValidator_InputWithInvalidCharacters_ReturnsFalse()
        {
            // Arrange
            var validator = new SedolValidator();

            // Act
            var result = validator.ValidateSedol("12@4567");

            // Assert
            result.IsValidSedol.Should().BeFalse();
            result.IsUserDefined.Should().BeFalse();
            result.ValidationDetails.Should().Be("SEDOL contains invalid characters");
        }

        [Fact]
        public void SedolValidator_InputWithSpaces_ReturnsFalse()
        {
            // Arrange
            var validator = new SedolValidator();

            // Act
            var result = validator.ValidateSedol("12 4567");

            // Assert
            result.IsValidSedol.Should().BeFalse();
            result.IsUserDefined.Should().BeFalse();
            result.ValidationDetails.Should().Be("SEDOL contains invalid characters");
        }
    }
}