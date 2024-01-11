namespace Platform.Application.Users.GetUsers;

public sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, Result<IEnumerable<UserDto>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);

        var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

        return Result.Success(usersDto);
    }
}
