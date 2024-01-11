using Platform.Application.Security;
using BC = BCrypt.Net.BCrypt;

namespace Platform.Infrastructure.Security;

internal sealed class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        ArgumentNullException.ThrowIfNull(password);

        return BC.HashPassword(password);
    }

    public bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        ArgumentNullException.ThrowIfNull(hashedPassword);
        ArgumentNullException.ThrowIfNull(providedPassword);

        return BC.Verify(providedPassword, hashedPassword);
    }
}
