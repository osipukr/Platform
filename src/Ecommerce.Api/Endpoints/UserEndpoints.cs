using Ecommerce.Application.Users.GetUserById;
using MediatR;

namespace Ecommerce.Api.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGroup("/api/users")
            .MapGet("/{id:int}", async (int id, ISender sender, CancellationToken cancellationToken) =>
            {
                var query = new GetUserByIdQuery(id);

                var result = await sender.Send(query, cancellationToken);

                return result.Match(Results.Ok, Results.BadRequest);
            });
    }
}
