using Platform.Application.Common;
using Platform.Domain.Common;

namespace Platform.Application.Users.GetUserById;

public record GetUserByIdQuery(int UserId) : IQuery<Result<UserDto>>;
