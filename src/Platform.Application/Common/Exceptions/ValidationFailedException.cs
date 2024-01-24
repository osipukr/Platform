using Platform.Domain.Common.Exceptions;

namespace Platform.Application.Common.Exceptions;

public sealed class ValidationFailedException : DomainException
{
    public ValidationFailedException(IEnumerable<ValidationFailureRecord> failures)
        : base("Validation.Failed", "Validation is failed", DomainExceptionKind.Validation)
    {
        Failures = failures;
    }

    public IEnumerable<ValidationFailureRecord> Failures { get; }
}
