namespace Platform.Domain.Users.Exceptions;

public sealed class UserNotFoundException : DomainException
{
    public UserNotFoundException(int userId) : base("User.NotFound",
        $"User with the ID = '{userId}' was not found",
        DomainExceptionKind.NotFound)
    {
    }
}
