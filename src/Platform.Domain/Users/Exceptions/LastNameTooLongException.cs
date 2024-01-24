namespace Platform.Domain.Users.Exceptions;

public sealed class LastNameTooLongException : DomainException
{
    public LastNameTooLongException(int maxLength)
        : base("LastName.TooLong",
            $"Last name cannot be longer than {maxLength} chars",
            DomainExceptionKind.Validation)
    {
    }
}
