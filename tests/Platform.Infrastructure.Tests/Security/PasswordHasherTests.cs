using FluentAssertions;
using FluentAssertions.Execution;
using Platform.Infrastructure.Security;

namespace Platform.Infrastructure.Tests.Security;

public class PasswordHasherTests
{
    private readonly PasswordHasher _passwordHasher = new();

    [Fact]
    public void HashPassword_Should_ThrowArgumentNullException_WhenPasswordIsNull()
    {
        // Act & Assert
        FluentActions
            .Invoking(() => _passwordHasher.HashPassword(null))
            .Should()
            .Throw<ArgumentNullException>()
            .Where(e => !string.IsNullOrWhiteSpace(e.ParamName));
    }

    [Theory]
    [InlineData(null, "test")]
    [InlineData("test", null)]
    public void VerifyHashedPassword_Should_ThrowArgumentNullException_WhenPasswordIsNull(
        string? hashedPassword,
        string? providedPassword)
    {
        // Act & Assert
        FluentActions
            .Invoking(() => _passwordHasher.VerifyHashedPassword(hashedPassword, providedPassword))
            .Should()
            .Throw<ArgumentNullException>()
            .Where(e => !string.IsNullOrWhiteSpace(e.ParamName));
    }

    [Fact]
    public void HashPasswordAndVerifyHashedPassword_Should_WorkAsExpected()
    {
        // Arrange
        const string sourcePassword = "some test password 123!";

        // Act
        string hashedPassword = _passwordHasher.HashPassword(sourcePassword);
        bool isVerified = _passwordHasher.VerifyHashedPassword(hashedPassword, sourcePassword);

        // Assert
        using (new AssertionScope())
        {
            hashedPassword.Should().NotBeEmpty().And.NotBe(sourcePassword);
            isVerified.Should().BeTrue();
        }
    }
}
