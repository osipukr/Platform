using Ecommerce.Application.Common;
using Ecommerce.Application.Security;
using Ecommerce.Application.Users;
using Ecommerce.Infrastructure.Data.Contexts;
using Ecommerce.Infrastructure.Data.Interceptors;
using Ecommerce.Infrastructure.Data.Repositories;
using Ecommerce.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure;

public static class DependencyInjection
{
    private const string DatabaseConnectionStringName = "DefaultConnection";

    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDataServices(configuration);
        services.AddSecurityServices();

        return services;
    }

    private static void AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(DatabaseConnectionStringName);

        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);

        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(connectionString);
            options.UseLoggerFactory(sp.GetService<ILoggerFactory>());
        });

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddTransient<IUserRepository, UserRepository>();
    }

    private static void AddSecurityServices(this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
    }
}
