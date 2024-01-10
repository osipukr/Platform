using Platform.Api.Endpoints;

namespace Platform.Api.Extensions;

public static class EndpointExtensions
{
    public static void MapEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api", () => "test");

        builder.MapUserEndpoints();
    }
}
