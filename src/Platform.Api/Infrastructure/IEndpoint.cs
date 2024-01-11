namespace Platform.Api.Infrastructure;

public interface IEndpoint
{
    void MapRoutes(IEndpointRouteBuilder builder);
}
