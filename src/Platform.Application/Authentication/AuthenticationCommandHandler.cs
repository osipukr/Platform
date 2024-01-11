using Platform.Application.Security;
using Platform.Application.Users;
using Platform.Domain.Users;

namespace Platform.Application.Authentication;

public sealed class AuthenticationCommandHandler : ICommandHandler<AuthenticationCommand, Result<AuthenticationResponse>>
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

    public async Task<Result<AuthenticationResponse>> Handle(AuthenticationCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        User? user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<AuthenticationResponse>(Error.Validation("Authentication.WrongEmail", ""));
        }

        bool isVerified = _passwordHasher.VerifyHashedPassword(user.PasswordHash, request.Password);

        if (!isVerified)
        {
            return Result.Failure<AuthenticationResponse>(Error.Validation("Authentication.WrongPassword", ""));
        }

        string token = await _tokenProvider.GenerateTokenAsync(user);

        return Result.Success(new AuthenticationResponse(token));
    }
}
