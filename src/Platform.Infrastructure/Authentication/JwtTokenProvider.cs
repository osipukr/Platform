using Platform.Application.Authentication;
using Platform.Domain.Users;

namespace Platform.Infrastructure.Authentication;

internal sealed class JwtTokenProvider : ITokenProvider
{
    public Task<string> GenerateTokenAsync(User user)
    {
        return Task.FromResult("TestToken");
    }
}
