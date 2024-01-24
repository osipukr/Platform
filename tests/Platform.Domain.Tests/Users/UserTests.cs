using Platform.Domain.Common;
using Platform.Domain.Users;
using Platform.Domain.Users.Events;
using Platform.Domain.Users.ValueObjects;

namespace Platform.Domain.Tests.Users;

public class UserTests
{
    private const int TestUserId = 1;

    [Fact]
    public void ChangeFirstName_Should_ReturnFalse_WhenNewNameIsTheSameAsOldOne()
    {
        // Arrange
        var firstName = FirstName.Create("first-name");
        var lastName = LastName.Create("last-name");
        var email = Email.Create("email@email.com");
        var passwordHash = string.Empty;
        var user = new User(TestUserId, firstName, lastName, email, passwordHash);

        // Act
        user.ChangeFirstName(firstName);

        // Assert
        using (new AssertionScope())
        {
            user.Id.Should().Be(TestUserId);
            user.FirstName.Should().BeEquivalentTo(firstName);
            user.LastName.Should().BeEquivalentTo(lastName);
            user.Email.Should().BeEquivalentTo(email);
            user.PasswordHash.Should().BeEquivalentTo(passwordHash);
            user.DomainEvents.Should().BeEmpty();
        }
    }

    [Fact]
    public void ChangeFirstName_Should_ReturnTrueAndAddDomainEvent_WhenNewNameIsNotTheSameAsOldOne()
    {
        // Arrange
        var firstName = FirstName.Create("first-name");
        var newFirstName = FirstName.Create("new-first-name");
        var lastName = LastName.Create("last-name");
        var email = Email.Create("email@email.com");
        var passwordHash = string.Empty;
        var user = new User(TestUserId, firstName, lastName, email, passwordHash);
        var expectedDomainEvent = new FirstNameChangedDomainEvent(TestUserId, firstName.Value, newFirstName.Value);

        // Act
        user.ChangeFirstName(newFirstName);

        // Assert
        using (new AssertionScope())
        {
            user.Id.Should().Be(TestUserId);
            user.FirstName.Should().BeEquivalentTo(newFirstName);
            user.LastName.Should().BeEquivalentTo(lastName);
            user.Email.Should().BeEquivalentTo(email);
            user.PasswordHash.Should().BeEquivalentTo(passwordHash);

            AssertSingleDomainEvent(user, expectedDomainEvent);
        }
    }

    [Fact]
    public void ChangeLastName_Should_ReturnFalse_WhenNewNameIsTheSameAsOldOne()
    {
        // Arrange
        var firstName = FirstName.Create("first-name");
        var lastName = LastName.Create("last-name");
        var email = Email.Create("email@email.com");
        var passwordHash = string.Empty;
        var user = new User(TestUserId, firstName, lastName, email, passwordHash);

        // Act
        user.ChangeLastName(lastName);

        // Assert
        using (new AssertionScope())
        {
            user.Id.Should().Be(TestUserId);
            user.FirstName.Should().BeEquivalentTo(firstName);
            user.LastName.Should().BeEquivalentTo(lastName);
            user.Email.Should().BeEquivalentTo(email);
            user.PasswordHash.Should().BeEquivalentTo(passwordHash);
            user.DomainEvents.Should().BeEmpty();
        }
    }

    [Fact]
    public void ChangeLastName_Should_ReturnTrueAndAddDomainEvent_WhenNewNameIsNotTheSameAsOldOne()
    {
        // Arrange
        var firstName = FirstName.Create("first-name");
        var lastName = LastName.Create("last-name");
        var newLastName = LastName.Create("new-last-name");
        var email = Email.Create("email@email.com");
        var passwordHash = string.Empty;
        var user = new User(TestUserId, firstName, lastName, email, passwordHash);
        var expectedDomainEvent = new LastNameChangedDomainEvent(TestUserId, lastName.Value, newLastName.Value);

        // Act
        user.ChangeLastName(newLastName);

        // Assert
        using (new AssertionScope())
        {
            user.Id.Should().Be(TestUserId);
            user.FirstName.Should().BeEquivalentTo(firstName);
            user.LastName.Should().BeEquivalentTo(newLastName);
            user.Email.Should().BeEquivalentTo(email);
            user.PasswordHash.Should().BeEquivalentTo(passwordHash);

            AssertSingleDomainEvent(user, expectedDomainEvent);
        }
    }

    [Fact]
    public void ChangeEmail_Should_ReturnFalse_WhenNewEmailIsTheSameAsOldOne()
    {
        // Arrange
        var firstName = FirstName.Create("first-name");
        var lastName = LastName.Create("last-name");
        var email = Email.Create("email@email.com");
        var passwordHash = string.Empty;
        var user = new User(TestUserId, firstName, lastName, email, passwordHash);

        // Act
        user.ChangeEmail(email);

        // Assert
        using (new AssertionScope())
        {
            user.Id.Should().Be(TestUserId);
            user.FirstName.Should().BeEquivalentTo(firstName);
            user.LastName.Should().BeEquivalentTo(lastName);
            user.Email.Should().BeEquivalentTo(email);
            user.PasswordHash.Should().BeEquivalentTo(passwordHash);
            user.DomainEvents.Should().BeEmpty();
        }
    }

    [Fact]
    public void ChangeEmail_Should_ReturnTrueAndAddDomainEvent_WhenNewEmailIsNotTheSameAsOldOne()
    {
        // Arrange
        var firstName = FirstName.Create("first-name");
        var lastName = LastName.Create("last-name");
        var email = Email.Create("email@email.com");
        var newEmail = Email.Create("new-email@email.com");
        var passwordHash = string.Empty;
        var user = new User(TestUserId, firstName, lastName, email, passwordHash);
        var expectedDomainEvent = new EmailChangedDomainEvent(TestUserId, email.Value, newEmail.Value);

        // Act
        user.ChangeEmail(newEmail);

        // Assert
        using (new AssertionScope())
        {
            user.Id.Should().Be(TestUserId);
            user.FirstName.Should().BeEquivalentTo(firstName);
            user.LastName.Should().BeEquivalentTo(lastName);
            user.Email.Should().BeEquivalentTo(newEmail);
            user.PasswordHash.Should().BeEquivalentTo(passwordHash);

            AssertSingleDomainEvent(user, expectedDomainEvent);
        }
    }

    private static void AssertSingleDomainEvent(BaseEntity entity, IDomainEvent domainEvent)
    {
        entity.DomainEvents
            .Should()
            .NotBeEmpty()
            .And.HaveCount(1)
            .And.Contain(domainEvent);
    }
}
