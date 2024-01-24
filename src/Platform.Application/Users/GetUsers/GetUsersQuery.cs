namespace Platform.Application.Users.GetUsers;

public sealed record GetUsersQuery : IQuery<IEnumerable<UserResponse>>;
