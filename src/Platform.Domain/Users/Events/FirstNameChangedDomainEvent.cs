namespace Platform.Domain.Users.Events;

public sealed record FirstNameChangedDomainEvent(
    int UserId,
    string PreviousValue,
    string CurrentValue) : IDomainEvent;
