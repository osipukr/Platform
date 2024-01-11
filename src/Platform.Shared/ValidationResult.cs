namespace Platform.Shared;

public sealed record ValidationResult : Result, IValidationResult
{
    public static readonly Error ValidationError = Error.Validation(
        "ValidationError",
        "A validation problem occurred");

    private ValidationResult(IEnumerable<Error> errors) : base(ValidationError)
    {
        Errors = errors;
    }

    public IEnumerable<Error> Errors { get; }

    public static ValidationResult WithErrors(IEnumerable<Error> errors) => new(errors);

    public static ValidationResult<T> WithErrors<T>(IEnumerable<Error> errors) => ValidationResult<T>.WithErrors(errors);

}
