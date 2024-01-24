namespace Platform.Api.Infrastructure;

internal interface IEndpoint
{
    void MapRoutes(IEndpointRouteBuilder builder);
}
