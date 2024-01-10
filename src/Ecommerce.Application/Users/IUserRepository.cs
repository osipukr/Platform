using Ecommerce.Domain.Users;

namespace Ecommerce.Application.Users;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
