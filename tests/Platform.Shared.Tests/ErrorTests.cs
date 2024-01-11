namespace Platform.Shared.Tests;

public class ErrorTests
{
    [Theory]
    [ClassData(typeof(ValidTestData))]
    public void Failure_Should_ReturnErrorWithFailureErrorType(string code, string description)
    {
        // Act
        var result = Error.Failure(code, description);

        // Assert
        AssertError(result, code, description, ErrorType.Failure);
    }

    [Theory]
    [ClassData(typeof(ValidTestData))]
    public void Validation_Should_ReturnErrorWithValidationErrorType(string code, string description)
    {
        // Act
        var result = Error.Validation(code, description);

        // Assert
        AssertError(result, code, description, ErrorType.Validation);
    }

    [Theory]
    [ClassData(typeof(ValidTestData))]
    public void NotFound_Should_ReturnErrorWithNotFoundErrorType(string code, string description)
    {
        // Act
        var result = Error.NotFound(code, description);

        // Assert
        AssertError(result, code, description, ErrorType.NotFound);
    }

    [Theory]
    [ClassData(typeof(ValidTestData))]
    public void Conflict_Should_ReturnErrorWithConflictErrorType(string code, string description)
    {
        // Act
        var result = Error.Conflict(code, description);

        // Assert
        AssertError(result, code, description, ErrorType.Conflict);
    }

    private static void AssertError(
        Error actualResult,
        string expectedCode,
        string expectedDescription,
        ErrorType expectedErrorType)
    {
        actualResult.Should().NotBeNull();

        using (new AssertionScope())
        {
            actualResult.Code.Should().Be(expectedCode);
            actualResult.Description.Should().Be(expectedDescription);
            actualResult.ErrorType.Should().Be(expectedErrorType);
        }
    }

    private sealed class ValidTestData : TheoryData<string, string>
    {
        public ValidTestData()
        {
            Add(string.Empty, string.Empty);
            Add("test-code", "test-description");
        }
    }
}
