using Platform.Domain.Common;
using MediatR;

namespace Platform.Application.Common;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent;
