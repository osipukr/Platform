using Platform.Domain.Users.Exceptions;
using Platform.Domain.Users.ValueObjects;

namespace Platform.Domain.Tests.Users.ValueObjects;

public class FirstNameTests
{
    private const int MaxLength = FirstName.MaxLength;

    [Theory]
    [ClassData(typeof(EmptyTestData))]
    public void Create_Should_ThrowFirstNameEmptyException_WhenValueIsEmpty(string? value)
    {
        // Act & Assert
        FluentActions
            .Invoking(() => FirstName.Create(value))
            .Should()
            .Throw<FirstNameEmptyException>();
    }

    [Fact]
    public void Create_Should_ThrowTooLongFirstNameException_WhenValueIsTooLong()
    {
        // Arrange
        var tooLongName = new string('a', MaxLength + 1);

        // Act & Assert
        FluentActions
            .Invoking(() => FirstName.Create(tooLongName))
            .Should()
            .Throw<FirstNameTooLongException>();
    }

    [Theory]
    [ClassData(typeof(ValidTestData))]
    public void Create_Should_ReturnFirstName_WhenValueIsValid(string? value, string expectedValue)
    {
        // Act
        var firstName = FirstName.Create(value);

        // Assert
        firstName.Should().NotBeNull();
        firstName.Value.Should().Be(expectedValue);
    }

    [Fact]
    public void Equals_Should_ReturnTrue_WhenValuesAreTheSame()
    {
        // Arrange
        var first = FirstName.Create("test");
        var second = FirstName.Create("test");

        // Act
        var result = first.Equals(second);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Equals_Should_ReturnFalse_WhenValuesAreNotTheSame()
    {
        // Arrange
        var first = FirstName.Create("test");
        var second = FirstName.Create("test-new");

        // Act
        var result = first.Equals(second);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void ToString_Should_ReturnValue()
    {
        // Arrange
        var firstName = FirstName.Create("test");

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
