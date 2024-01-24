namespace Platform.Domain.Users.Exceptions;

public sealed class FirstNameTooLongException : DomainException
{
    public FirstNameTooLongException(int maxLength)
        : base("FirstName.TooLong",
            $"First name cannot be longer than {maxLength} chars",
            DomainExceptionKind.Validation)
    {
    }
}
