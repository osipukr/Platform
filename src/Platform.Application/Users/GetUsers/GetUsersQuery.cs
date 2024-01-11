namespace Platform.Application.Users.GetUsers;

public record GetUsersQuery : IQuery<Result<IEnumerable<UserDto>>>;
