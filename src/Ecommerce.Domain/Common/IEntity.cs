namespace Ecommerce.Domain.Common;

public interface IEntity
{
    int Id { get; }

    IEnumerable<IDomainEvent> DomainEvents { get; }

    void AddDomainEvent(IDomainEvent domainEvent);

    void RemoveDomainEvent(IDomainEvent domainEvent);

    void ClearDomainEvents();
}
