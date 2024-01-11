using Platform.Application.Authentication;
using Platform.Domain.Users;

namespace Platform.Infrastructure.Authentication;

internal sealed class JwtTokenProvider : ITokenProvider
{
    public async Task<string> GenerateTokenAsync(User user)
    {
        return "TestToken";
    }
}
