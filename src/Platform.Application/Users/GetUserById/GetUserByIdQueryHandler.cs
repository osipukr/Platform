using Platform.Domain.Users;

namespace Platform.Application.Users.GetUserById;

public sealed class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, Result<UserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<UserDto>(UserErrors.NotFound(request.UserId));
        }

        var userDto = _mapper.Map<UserDto>(user);

        return Result.Success(userDto);
    }
}
