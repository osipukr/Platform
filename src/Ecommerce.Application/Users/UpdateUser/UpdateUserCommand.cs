using Ecommerce.Application.Common;
using Ecommerce.Domain.Common;

namespace Ecommerce.Application.Users.UpdateUser;

public record UpdateUserCommand(int UserId, string FirstName, string LastName) : ICommand<Result>;
