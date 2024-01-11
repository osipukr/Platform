namespace Platform.Shared;

public sealed record ValidationResult<T> : Result<T>, IValidationResult
{
    private ValidationResult(IEnumerable<Error> errors) : base(ValidationResult.ValidationError)
    {
        Errors = errors;
    }

    public IEnumerable<Error> Errors { get; }

    public static ValidationResult<T> WithErrors(IEnumerable<Error> errors) => new(errors);
}
