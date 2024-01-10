using Platform.Domain.Common;

namespace Platform.Domain.Users.Events;

public sealed record LastNameChangedDomainEvent(
    int UserId,
    string PreviousValue,
    string CurrentValue) : IDomainEvent;
