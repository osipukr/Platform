using Ecommerce.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Data.Contexts;

internal interface IApplicationDbContext
{
    DbSet<User> Users { get; }
}
