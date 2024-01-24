using Platform.Application.Security;
using Platform.Application.Users;
using Platform.Domain.Authentication.Exceptions;

namespace Platform.Application.Authentication;

public sealed class AuthenticationCommandHandler : ICommandHandler<AuthenticationCommand, AuthenticationResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenProvider _tokenProvider;

    public AuthenticationCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenProvider tokenProvider)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenProvider = tokenProvider;
    }

    public async Task<AuthenticationResponse> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            throw new AuthenticationFailedException();
        }

        var isVerified = _passwordHasher.VerifyHashedPassword(user.PasswordHash, request.Password);

        if (!isVerified)
        {
            throw new AuthenticationFailedException();
        }

        var token = await _tokenProvider.GenerateTokenAsync(user);

        var response = new AuthenticationResponse(token);

        return response;
    }
}
