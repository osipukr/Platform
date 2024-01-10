using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Users;

public class User : BaseEntity
{
    private User()
    {
    }

    public User(string email, string passwordHash)
    {
        Email = email;
        PasswordHash = passwordHash;
    }

    public string Email { get; } = string.Empty;

    public string PasswordHash { get; } = string.Empty;
}
