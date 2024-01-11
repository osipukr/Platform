using Platform.Api.Infrastructure;

namespace Platform.Api.Endpoints;

public sealed class ApiEndpoints : IEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api", () => "test");
    }
}
