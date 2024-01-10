using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Users.Events;

public sealed record LastNameChangedDomainEvent(
    int UserId,
    string PreviousValue,
    string CurrentValue) : IDomainEvent;
