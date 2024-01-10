using Ecommerce.Application.Common;
using Ecommerce.Domain.Common;
using Ecommerce.Domain.Users;

namespace Ecommerce.Application.Users.UpdateUser;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Result>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(request.UserId));
        }

        var firstName = FirstName.Create(request.FirstName);
        var lastName = LastName.Create(request.LastName);

        if (firstName.IsFailure)
        {
            return Result.Failure(firstName.Error);
        }

        if (lastName.IsFailure)
        {
            return Result.Failure(lastName.Error);
        }

        user.ChangeFirstName(firstName.Value);
        user.ChangeLastName(lastName.Value);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
