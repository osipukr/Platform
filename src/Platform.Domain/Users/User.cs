﻿using Platform.Domain.Users.Events;
using Platform.Domain.Users.ValueObjects;

namespace Platform.Domain.Users;

public sealed class User : BaseEntity
{
    private User()
    {
    }

    public User(FirstName firstName, LastName lastName, Email email, string passwordHash)
        : this(default, firstName, lastName, email, passwordHash)
    {
    }

    public User(int id, FirstName firstName, LastName lastName, Email email, string passwordHash)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
    }

    public FirstName FirstName { get; private set; }

    public LastName LastName { get; private set; }

    public Email Email { get; private set; }

    public string PasswordHash { get; }

    public void ChangeFirstName(FirstName firstName)
    {
        if (FirstName.Equals(firstName))
        {
            return;
        }

        var previousValue = FirstName.Value;

        FirstName = firstName;

        AddDomainEvent(new FirstNameChangedDomainEvent(Id, previousValue, FirstName.Value));
    }

    public void ChangeLastName(LastName lastName)
    {
        if (LastName.Equals(lastName))
        {
            return;
        }

        var previousValue = LastName.Value;

        LastName = lastName;

        AddDomainEvent(new LastNameChangedDomainEvent(Id, previousValue, LastName.Value));
    }

    public void ChangeEmail(Email email)
    {
        if (Email.Equals(email))
        {
            return;
        }

        var previousValue = Email.Value;

        Email = email;

        AddDomainEvent(new EmailChangedDomainEvent(Id, previousValue, Email.Value));
    }
}
