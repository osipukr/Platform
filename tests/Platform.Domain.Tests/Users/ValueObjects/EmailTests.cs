using Platform.Domain.Users.ValueObjects;

namespace Platform.Domain.Tests.Users.ValueObjects;

public class EmailTests
{
    [Theory]
    [ClassData(typeof(ValidTestData))]
    public void Create_Should_ReturnSuccess_WhenValueIsValid(string? value, string expectedValue)
    {
        // Act
        var result = Email.Create(value);

        // Assert
        var firstName = result.Value;

        firstName.Value.Should().Be(expectedValue);
    }

    private sealed class ValidTestData : TheoryData<string?, string>
    {
        public ValidTestData()
        {
            Add("email@email.com", "email@email.com");
            Add("   email@email.com", "email@email.com");
            Add("email@email.com   ", "email@email.com");
        }
    }
}
