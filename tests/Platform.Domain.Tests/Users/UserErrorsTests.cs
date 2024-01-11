using Platform.Domain.Users;
using Platform.Shared;

namespace Platform.Domain.Tests.Users;

public class UserErrorsTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void NotFound_Should_ReturnNotFoundError(int userId)
    {
        // Act
        Error error = UserErrors.NotFound(userId);

        // Assert
        using (new AssertionScope())
        {
            error.Code.Should().Be("User.NotFound");
            error.Description.Should().NotBeEmpty().And.Contain($"{userId}");
            error.ErrorType.Should().Be(ErrorType.NotFound);
        }
    }

    [Fact]
    public void FirstNameEmpty_Should_ReturnValidationError()
    {
        // Act
        Error error = UserErrors.FirstNameEmpty;

        // Assert
        using (new AssertionScope())
        {
            error.Code.Should().Be("FirstName.Empty");
            error.Description.Should().NotBeEmpty();
            error.ErrorType.Should().Be(ErrorType.Validation);
        }
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void FirstNameTooLong_Should_ReturnValidationError(int maxLength)
    {
        // Act
        Error error = UserErrors.FirstNameTooLong(maxLength);

        // Assert
        using (new AssertionScope())
        {
            error.Code.Should().Be("FirstName.TooLong");
            error.Description.Should().NotBeEmpty().And.Contain($"{maxLength}");
            error.ErrorType.Should().Be(ErrorType.Validation);
        }
    }

    [Fact]
    public void LastNameEmpty_Should_ReturnValidationError()
    {
        // Act
        Error error = UserErrors.LastNameEmpty;

        // Assert
        using (new AssertionScope())
        {
            error.Code.Should().Be("LastName.Empty");
            error.Description.Should().NotBeEmpty();
            error.ErrorType.Should().Be(ErrorType.Validation);
        }
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    public void LastNameTooLong_Should_ReturnValidationError(int maxLength)
    {
        // Act
        Error error = UserErrors.LastNameTooLong(maxLength);

        // Assert
        using (new AssertionScope())
        {
            error.Code.Should().Be("LastName.TooLong");
            error.Description.Should().NotBeEmpty().And.Contain($"{maxLength}");
            error.ErrorType.Should().Be(ErrorType.Validation);
        }
    }

    [Fact]
    public void EmailEmpty_Should_ReturnValidationError()
    {
        // Act
        Error error = UserErrors.EmailEmpty;

        // Assert
        using (new AssertionScope())
        {
            error.Code.Should().Be("Email.Empty");
            error.Description.Should().NotBeEmpty();
            error.ErrorType.Should().Be(ErrorType.Validation);
        }
    }
}
