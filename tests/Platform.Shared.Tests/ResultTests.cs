namespace Platform.Shared.Tests;

public class ResultTests
{
    [Fact]
    public void Success_Should_ReturnSuccessResult()
    {
        // Act
        var result = Result.Success();

        // Assert
        result.Should().NotBeNull();

        using (new AssertionScope())
        {
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();

            FluentActions
                .Invoking(() => result.Error)
                .Should()
                .Throw<InvalidOperationException>()
                .Where(e => !string.IsNullOrWhiteSpace(e.Message));
        }
    }

    [Fact]
    public void Failure_Should_ReturnFailureResult()
    {
        // Act
        var error = Error.NotFound(string.Empty, string.Empty);
        var result = Result.Failure(error);

        // Assert
        result.Should().NotBeNull();

        using (new AssertionScope())
        {
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(error);
        }
    }

    [Fact]
    public void Match_Should_CallOnSuccess_WhenResultIsSuccess()
    {
        // Arrange
        const string successResult = "success";
        const string failureResult = "failure";

        var result = Result.Success();

        // Act
        var actualResult = result.Match(() => successResult, _ => failureResult);

        // Assert
        actualResult.Should().Be(successResult);
    }

    [Fact]
    public void Match_Should_CallOnFailure_WhenResultIsFailure()
    {
        // Arrange
        const string successResult = "success";
        const string failureResult = "failure";

        var error = Error.NotFound(string.Empty, string.Empty);
        var result = Result.Failure(error);

        // Act
        var actualResult = result.Match(() => successResult, _ => failureResult);

        // Assert
        actualResult.Should().Be(failureResult);
    }
}
