using Platform.Domain.Common;
using Platform.Domain.Users.Events;

namespace Platform.Domain.Users;

public sealed class User : BaseEntity
{
    public User(FirstName firstName, LastName lastName, Email email, string passwordHash)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
    }

    public FirstName FirstName { get; private set; }

    public LastName LastName { get; private set; }

    public Email Email { get; private set; }

    public string PasswordHash { get; }

    public bool ChangeFirstName(FirstName firstName)
    {
        if (FirstName.Equals(firstName))
        {
            return false;
        }

        var previousValue = FirstName.Value;

        FirstName = firstName;

        AddDomainEvent(new FirstNameChangedDomainEvent(Id, previousValue, FirstName.Value));

        return true;
    }

    public bool ChangeLastName(LastName lastName)
    {
        if (LastName.Equals(lastName))
        {
            return false;
        }

        var previousValue = LastName.Value;

        LastName = lastName;

        AddDomainEvent(new LastNameChangedDomainEvent(Id, previousValue, LastName.Value));

        return true;
    }

    public bool ChangeEmail(Email email)
    {
        if (Email.Equals(email))
        {
            return false;
        }

        var previousValue = Email.Value;

        Email = email;

        AddDomainEvent(new EmailChangedDomainEvent(Id, previousValue, Email.Value));

        return true;
    }
}
