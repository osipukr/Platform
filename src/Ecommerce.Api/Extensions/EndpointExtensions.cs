using Ecommerce.Api.Endpoints;

namespace Ecommerce.Api.Extensions;

public static class EndpointExtensions
{
    public static void MapEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api", () => "test");

        builder.MapUserEndpoints();
    }
}
