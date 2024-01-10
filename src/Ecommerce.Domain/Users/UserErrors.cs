using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Users;

public static class UserErrors
{
    public static Error NotFound(int userId) => Error.NotFound(
        "User.NotFound",
        $"User with the ID = '{userId}' was not found");
}
