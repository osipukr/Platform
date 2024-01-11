using MediatR;
using Microsoft.AspNetCore.Mvc;
using Platform.Api.Infrastructure;
using Platform.Application.Authentication;

namespace Platform.Api.Endpoints;

public sealed class AuthenticationEndpoints : IEndpoint
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

            return result.Match(Results.Ok, Results.BadRequest);
        });
    }
}
