using Platform.Api.Infrastructure;

namespace Platform.Api.Extensions;

internal static class EndpointExtensions
{
    internal static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services.Scan(scan =>
            scan.FromAssemblyOf<IEndpoint>()
                .AddClasses(classes => classes.AssignableTo<IEndpoint>())
                .AsImplementedInterfaces()
        );

        return services;
    }

    internal static void MapEndpoints(this IEndpointRouteBuilder builder)
    {
        var endpoints = builder.ServiceProvider.GetServices<IEndpoint>().ToArray();

        foreach (var endpoint in endpoints)
        {
            endpoint.MapRoutes(builder);
        }
    }
}
