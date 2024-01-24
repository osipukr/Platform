using Platform.Domain.Users.Exceptions;
using Platform.Domain.Users.ValueObjects;

namespace Platform.Application.Users.UpdateUser;

public sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            throw new UserNotFoundException(request.UserId);
        }

        var firstName = FirstName.Create(request.FirstName);
        var lastName = LastName.Create(request.LastName);

        user.ChangeFirstName(firstName);
        user.ChangeLastName(lastName);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
