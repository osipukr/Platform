namespace Platform.Domain.Users.Exceptions;

public sealed class EmailEmptyException : DomainException
{
    public EmailEmptyException()
        : base("Email.Empty", "Email cannot be empty", DomainExceptionKind.Validation)
    {
    }
}
