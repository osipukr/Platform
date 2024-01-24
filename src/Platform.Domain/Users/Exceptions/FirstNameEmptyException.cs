namespace Platform.Domain.Users.Exceptions;

public sealed class FirstNameEmptyException : DomainException
{
    public FirstNameEmptyException()
        : base("FirstName.Empty", "First name cannot be empty", DomainExceptionKind.Validation)
    {
    }
}
