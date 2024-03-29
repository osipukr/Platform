﻿using Platform.Domain.Users.Events;

namespace Platform.Application.Users.EventHandlers;

public sealed class FirstNameChangedDomainEventHandler : IDomainEventHandler<FirstNameChangedDomainEvent>
{
    private readonly ILogger<FirstNameChangedDomainEventHandler> _logger;

    public FirstNameChangedDomainEventHandler(ILogger<FirstNameChangedDomainEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(FirstNameChangedDomainEvent notification, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(notification);

        _logger.LogInformation("First name was changed from '{PreviousValue}' to '{CurrentValue}' for the user with ID = '{UserId}'",
            notification.PreviousValue,
            notification.CurrentValue,
            notification.UserId);

        return Task.CompletedTask;
    }
}
