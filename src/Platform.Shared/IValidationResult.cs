namespace Platform.Shared;

public interface IValidationResult
{
    IEnumerable<Error> Errors { get; }
}
