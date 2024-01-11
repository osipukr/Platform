using Platform.Domain.Users;

namespace Platform.Infrastructure.Data.Contexts;

internal interface IApplicationDbContext
{
    DbSet<User> Users { get; }
}
