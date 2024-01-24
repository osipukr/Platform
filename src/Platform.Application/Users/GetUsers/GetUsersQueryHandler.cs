﻿namespace Platform.Application.Users.GetUsers;

public sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, IEnumerable<UserResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);

        var usersResponse = _mapper.Map<IEnumerable<UserResponse>>(users);

        return usersResponse;
    }
}
