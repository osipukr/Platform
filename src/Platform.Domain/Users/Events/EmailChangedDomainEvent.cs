namespace Platform.Domain.Users.Events;

public sealed record EmailChangedDomainEvent(
    int UserId,
    string PreviousValue,
    string CurrentValue) : IDomainEvent;
