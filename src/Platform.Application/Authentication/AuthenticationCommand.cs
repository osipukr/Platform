namespace Platform.Application.Authentication;

public sealed record AuthenticationCommand(
    string Email,
    string Password) : ICommand<Result<AuthenticationResponse>>;
