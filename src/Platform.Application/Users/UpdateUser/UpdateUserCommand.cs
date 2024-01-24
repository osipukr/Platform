namespace Platform.Application.Users.UpdateUser;

public sealed record UpdateUserCommand(
    int UserId,
    string FirstName,
    string LastName) : ICommand;
