using Platform.Domain.Users;
using Platform.Domain.Users.ValueObjects;

namespace Platform.Domain.Tests.Users.ValueObjects;

public class FirstNameTests
{
    private const int MaxLength = FirstName.MaxLength;

    [Theory]
    [ClassData(typeof(EmptyTestData))]
    public void Create_Should_ReturnError_WhenValueIsEmpty(string? value)
    {
        // Act
        var result = FirstName.Create(value);

        // Assert
        result.Error.Should().Be(UserErrors.FirstNameEmpty);
    }

    [Fact]
    public void Create_Should_ReturnError_WhenValueIsTooLong()
    {
        // Arrange
        var tooLongString = new string('a', MaxLength + 1);

        // Act
        var result = FirstName.Create(tooLongString);

        // Assert
        result.Error.Should().Be(UserErrors.FirstNameTooLong(MaxLength));
    }

    [Theory]
    [ClassData(typeof(ValidTestData))]
    public void Create_Should_ReturnSuccess_WhenValueIsValid(string? value, string expectedValue)
    {
        // Act
        var result = FirstName.Create(value);

        // Assert
        var firstName = result.Value;

        firstName.Value.Should().Be(expectedValue);
    }

    [Fact]
    public void Equals_Should_ReturnTrue_WhenValuesAreTheSame()
    {
        // Arrange
        var first = FirstName.Create("test").Value;
        var second = FirstName.Create("test").Value;

        // Act
        var result = first.Equals(second);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Equals_Should_ReturnFalse_WhenValuesAreNotTheSame()
    {
        // Arrange
        var first = FirstName.Create("test").Value;
        var second = FirstName.Create("test-new").Value;

        // Act
        var result = first.Equals(second);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void ToString_Should_ReturnValue()
    {
        // Arrange
        var firstName = FirstName.Create("test").Value;

        // Act
        var result = firstName.ToString();

        // Assert
        result.Should().Be(firstName.Value);
    }

    private sealed class EmptyTestData : TheoryData<string?>
    {
        public EmptyTestData()
        {
            Add(null);
            Add(string.Empty);
            Add("     ");
        }
    }

    private sealed class ValidTestData : TheoryData<string?, string>
    {
        public ValidTestData()
        {
            var maxLengthString = new string('a', MaxLength);

            Add("test", "test");
            Add("   Test", "Test");
            Add("TEST   ", "TEST");
            Add(maxLengthString, maxLengthString);
        }
    }
}
