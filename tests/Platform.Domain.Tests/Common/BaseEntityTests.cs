using Platform.Domain.Common;

namespace Platform.Domain.Tests.Common;

public class BaseEntityTests
{
    [Fact]
    public void Ctor_Should_HaveNoDomainsEvents()
    {
        // Arrange
        var entity = CreateEntity();

        // Act & Assert
        entity.DomainEvents.Should().BeEmpty();
    }

    [Fact]
    public void AddDomainEvent_Should_InsertDomainEventToTheCollection()
    {
        // Arrange
        IDomainEvent[] domainEvents = [CreateDomainEvent(), CreateDomainEvent()];

        // Act
        BaseEntity entity = CreateEntityAndPopulateDomainEvents(domainEvents);

        // Assert
        entity.DomainEvents
            .Should()
            .NotBeEmpty()
            .And.HaveCount(domainEvents.Length)
            .And.Contain(domainEvents);
    }

    [Fact]
    public void RemoveDomainEvent_Should_NotThrowAnyException_WhenNoDomainEvents()
    {
        // Arrange
        var entity = CreateEntity();
        var domainEvent = CreateDomainEvent();

        // Act & Assert
        FluentActions
            .Invoking(() => entity.RemoveDomainEvent(domainEvent))
            .Should()
            .NotThrow();
    }

    [Fact]
    public void RemoveDomainEvent_Should_DeleteDomainEventFromTheCollection_WhenTheAreDomainEvents()
    {
        // Arrange
        IDomainEvent[] domainEvents = [CreateDomainEvent(), CreateDomainEvent()];
        BaseEntity entity = CreateEntityAndPopulateDomainEvents(domainEvents);

        // Act
        entity.RemoveDomainEvent(domainEvents[0]);

        // Assert
        entity.DomainEvents
            .Should()
            .NotBeEmpty()
            .And.HaveCount(domainEvents.Length - 1)
            .And.Contain(domainEvents.Skip(1));
    }

    [Fact]
    public void ClearDomainEvents_Should_NotThrowAnyException_WhenNoDomainEvents()
    {
        // Arrange
        var entity = CreateEntity();

        // Act & Assert
        FluentActions
            .Invoking(entity.ClearDomainEvents)
            .Should()
            .NotThrow();
    }

    [Fact]
    public void ClearDomainEvents_Should_DeleteAllDomainEventsFromTheCollection_WhenTheAreDomainEvents()
    {
        // Arrange
        IDomainEvent[] domainEvents = [CreateDomainEvent(), CreateDomainEvent()];
        BaseEntity entity = CreateEntityAndPopulateDomainEvents(domainEvents);

        // Act
        entity.ClearDomainEvents();

        // Assert
        entity.DomainEvents.Should().BeEmpty();
    }

    private static BaseEntity CreateEntity()
    {
        return new FakeEntity();
    }

    private static IDomainEvent CreateDomainEvent()
    {
        return new FakeDomainEvent();
    }

    private static BaseEntity CreateEntityAndPopulateDomainEvents(params IDomainEvent[] domainEvents)
    {
        var entity = CreateEntity();

        foreach (var domainEvent in domainEvents)
        {
            entity.AddDomainEvent(domainEvent);
        }

        return entity;
    }

    private sealed class FakeEntity : BaseEntity;

    private sealed class FakeDomainEvent : IDomainEvent;
}
