namespace Platform.Domain.Authentication.Exceptions;

public sealed class AuthenticationFailedException : DomainException
{
    public AuthenticationFailedException()
        : base("Authentication.Failed", "Authentication is failed", DomainExceptionKind.Failure)
    {
    }
}
