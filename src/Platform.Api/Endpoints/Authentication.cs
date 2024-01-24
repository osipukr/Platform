using MediatR;
using Microsoft.AspNetCore.Mvc;
using Platform.Api.Infrastructure;
using Platform.Application.Authentication;

namespace Platform.Api.Endpoints;

internal sealed class Authentication : IEndpoint
{
    public void MapRoutes(IEndpointRouteBuilder builder)
    {
        builder.MapPost("/api/auth", async (
            [FromBody] AuthenticationRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new AuthenticationCommand(request.Email, request.Password);

            var result = await sender.Send(command, cancellationToken);

            return Results.Ok(result);
        });
    }
}
