namespace Platform.Application.Users.UpdateUser;

public record UpdateUserCommand(int UserId, string FirstName, string LastName) : ICommand<Result>;
