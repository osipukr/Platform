using Platform.Application.Users.GetUserById;
using Platform.Application.Users.GetUsers;
using Platform.Application.Users.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Platform.Api.Infrastructure;

namespace Platform.Api.Endpoints;

public sealed class UserEndpoints : IEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder builder)
    {
        var routeGroup = builder.MapGroup("/api/users");

        routeGroup.MapGet("/", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetUsersQuery();

            var result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, Results.BadRequest);
        });

        routeGroup.MapGet("/{id:int}", async (int id, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetUserByIdQuery(id);

            var result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, Results.BadRequest);
        });

        routeGroup.MapPut("/{id:int}",
            async (int id,
                [FromBody] UpdateUserRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new UpdateUserCommand(id, request.FirstName, request.LastName);

                var result = await sender.Send(command, cancellationToken);

                return result.Match(() => Results.Ok(), Results.BadRequest);
            });
    }
}
