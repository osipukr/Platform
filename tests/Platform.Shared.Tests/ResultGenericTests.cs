namespace Platform.Shared.Tests;

public class ResultGenericTests
{
    [Fact]
    public void Success_Should_ReturnSuccessResult()
    {
        // Arrange
        const string expectedValue = "test";

        // Act
        var result = Result.Success(expectedValue);

        // Assert
        using (new AssertionScope())
        {
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            result.Value.Should().Be(expectedValue);

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
        var result = Result.Failure<string>(error);

        // Assert
        using (new AssertionScope())
        {
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(error);

            FluentActions
                .Invoking(() => result.Value)
                .Should()
                .Throw<InvalidOperationException>()
                .Where(e => !string.IsNullOrWhiteSpace(e.Message));
        }
    }

    [Fact]
    public void Match_Should_CallOnSuccess_WhenResultIsSuccess()
    {
        // Arrange
        const string successResult = "success";
        const string failureResult = "failure";

        var result = Result.Success(string.Empty);

        // Act
        var actualResult = result.Match(_ => successResult, _ => failureResult);

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
        var result = Result.Failure<string>(error);

        // Act
        var actualResult = result.Match(_ => successResult, _ => failureResult);

        // Assert
        actualResult.Should().Be(failureResult);
    }
}
