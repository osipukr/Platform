using Ecommerce.Application.Common;
using Ecommerce.Domain.Common;

namespace Ecommerce.Application.Users.GetUsers;

public record GetUsersQuery : IQuery<Result<IEnumerable<UserDto>>>;
