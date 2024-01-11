using Platform.Domain.Common;

namespace Platform.Application.Common;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent;
