using Platform.Domain.Users;

namespace Platform.Application.Authentication;

public interface ITokenProvider
{
    Task<string> GenerateTokenAsync(User user);
}
