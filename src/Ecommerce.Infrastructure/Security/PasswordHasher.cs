using Ecommerce.Application.Security;
using BC = BCrypt.Net.BCrypt;

namespace Ecommerce.Infrastructure.Security;

internal sealed class PasswordHasher : IPasswordHasher
{
    private const int Cost = 17;

    public string HashPassword(string password)
    {
        ArgumentNullException.ThrowIfNull(password);

        return BC.HashPassword(password, Cost);
    }

    public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        ArgumentNullException.ThrowIfNull(hashedPassword);
        ArgumentNullException.ThrowIfNull(providedPassword);

        return BC.Verify(providedPassword, hashedPassword);
    }
}
