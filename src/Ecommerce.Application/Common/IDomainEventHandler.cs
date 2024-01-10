using Ecommerce.Domain.Common;
using MediatR;

namespace Ecommerce.Application.Common;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent;
