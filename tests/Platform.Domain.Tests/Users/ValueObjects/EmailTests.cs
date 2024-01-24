using Platform.Domain.Users.ValueObjects;

namespace Platform.Domain.Tests.Users.ValueObjects;

public class EmailTests
{
    [Theory]
    [ClassData(typeof(ValidTestData))]
    public void Create_Should_ReturnEmail_WhenValueIsValid(string? value, string expectedValue)
    {
        // Act
        var email = Email.Create(value);

        // Assert
        email.Should().NotBeNull();
        email.Value.Should().Be(expectedValue);
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
