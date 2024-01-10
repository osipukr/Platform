using Ecommerce.Application.Common;
using Ecommerce.Domain.Users.Events;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.Users.EventHandlers;

public sealed class LastNameChangedDomainEventHandler : IDomainEventHandler<LastNameChangedDomainEvent>
{
    private readonly ILogger<LastNameChangedDomainEventHandler> _logger;

    public LastNameChangedDomainEventHandler(ILogger<LastNameChangedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(LastNameChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(notification);

        _logger.LogInformation("Last name was changed from '{PreviousValue}' to '{CurrentValue}' for the user with ID = '{UserId}'",
            notification.PreviousValue,
            notification.CurrentValue,
            notification.UserId);

        return Task.CompletedTask;
    }
}
