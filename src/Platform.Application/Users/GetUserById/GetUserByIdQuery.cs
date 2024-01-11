namespace Platform.Application.Users.GetUserById;

public record GetUserByIdQuery(int UserId) : IQuery<Result<UserDto>>;
