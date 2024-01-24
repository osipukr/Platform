using Platform.Domain.Users.Exceptions;
using Platform.Domain.Users.ValueObjects;

namespace Platform.Domain.Tests.Users.ValueObjects;

public class LastNameTests
{
    private const int MaxLength = LastName.MaxLength;

    [Theory]
    [ClassData(typeof(EmptyTestData))]
    public void Create_Should_ThrowLastNameEmptyException_WhenValueIsEmpty(string? value)
    {
        // Act & Assert
        FluentActions
            .Invoking(() => LastName.Create(value))
            .Should()
            .Throw<LastNameEmptyException>();
    }

    [Fact]
    public void Create_Should_ThrowLastNameTooLongException_WhenValueIsTooLong()
    {
        // Arrange
        var tooLongName = new string('a', MaxLength + 1);

        // Act & Assert
        FluentActions
            .Invoking(() => LastName.Create(tooLongName))
            .Should()
            .Throw<LastNameTooLongException>();
    }

    [Theory]
    [ClassData(typeof(ValidTestData))]
    public void Create_Should_ReturnLastName_WhenValueIsValid(string? value, string expectedValue)
    {
        // Act
        var lastName = LastName.Create(value);

        // Assert
        lastName.Should().NotBeNull();
        lastName.Value.Should().Be(expectedValue);
    }

    [Fact]
    public void Equals_Should_ReturnTrue_WhenValuesAreTheSame()
    {
        // Arrange
        var first = LastName.Create("test");
        var second = LastName.Create("test");

        // Act
        var result = first.Equals(second);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Equals_Should_ReturnFalse_WhenValuesAreNotTheSame()
    {
        // Arrange
        var first = LastName.Create("test");
        var second = LastName.Create("test-new");

        // Act
        var result = first.Equals(second);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void ToString_Should_ReturnValue()
    {
        // Arrange
        var lastName = LastName.Create("test");

        // Act
        var result = lastName.ToString();

        // Assert
        result.Should().Be(lastName.Value);
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
