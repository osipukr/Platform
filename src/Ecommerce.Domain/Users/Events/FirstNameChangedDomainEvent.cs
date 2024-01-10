using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Users.Events;

public sealed record FirstNameChangedDomainEvent(
    int UserId,
    string PreviousValue,
    string CurrentValue) : IDomainEvent;
