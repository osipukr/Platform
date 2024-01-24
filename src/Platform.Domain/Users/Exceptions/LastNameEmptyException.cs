namespace Platform.Domain.Users.Exceptions;

public sealed class LastNameEmptyException : DomainException
{
    public LastNameEmptyException()
        : base("LastName.Empty", "Last name cannot be empty", DomainExceptionKind.Validation)
    {
    }
}
