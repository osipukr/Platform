using Ecommerce.Application.Common;
using Ecommerce.Domain.Common;

namespace Ecommerce.Application.Users.GetUserById;

public record GetUserByIdQuery(int UserId) : IQuery<Result<UserDto>>;
