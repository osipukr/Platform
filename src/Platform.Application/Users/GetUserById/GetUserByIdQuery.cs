namespace Platform.Application.Users.GetUserById;

public sealed record GetUserByIdQuery(int UserId) : IQuery<UserResponse>;
