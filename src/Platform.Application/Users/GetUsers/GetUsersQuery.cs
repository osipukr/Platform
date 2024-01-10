using Platform.Application.Common;
using Platform.Domain.Common;

namespace Platform.Application.Users.GetUsers;

public record GetUsersQuery : IQuery<Result<IEnumerable<UserDto>>>;
