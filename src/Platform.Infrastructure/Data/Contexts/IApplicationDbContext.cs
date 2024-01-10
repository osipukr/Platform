using Platform.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Platform.Infrastructure.Data.Contexts;

internal interface IApplicationDbContext
{
    DbSet<User> Users { get; }
}
