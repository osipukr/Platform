using Platform.Application.Security;
using Platform.Application.Users;
using Platform.Infrastructure.Data.Contexts;
using Platform.Infrastructure.Data.Interceptors;
using Platform.Infrastructure.Data.Repositories;
using Platform.Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Platform.Application.Authentication;
using Platform.Infrastructure.Authentication;

namespace Platform.Infrastructure;

public static class DependencyInjection
{
    private const string DatabaseConnectionStringName = "DefaultConnection";

    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDataServices(configuration);
        services.AddSecurityServices();
        services.AddAuthenticationServices();

        return services;
    }

    private static void AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(DatabaseConnectionStringName);

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException($"Connection string '{DatabaseConnectionStringName}' was not found");
        }

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

    private static void AddAuthenticationServices(this IServiceCollection services)
    {
        services.AddTransient<ITokenProvider, JwtTokenProvider>();
    }
}
