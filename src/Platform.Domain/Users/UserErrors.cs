using Platform.Domain.Common;

namespace Platform.Domain.Users;

public static class UserErrors
{
    public static Error NotFound(int userId) => Error.NotFound(
        "User.NotFound",
        $"User with the ID = '{userId}' was not found");

    public static readonly Error FirstNameEmpty = Error.Validation(
        "FirstName.Empty",
        "First name cannot be empty");

    public static Error FirstNameTooLong(int maxLength) => Error.Validation(
        "FirstName.TooLong",
        $"First name cannot be longer than {maxLength} chars");

    public static readonly Error LastNameEmpty = Error.Validation(
        "LastName.Empty",
        "Last name cannot be empty");

    public static Error LastNameTooLong(int maxLength) => Error.Validation(
        "LastName.TooLong",
        $"Last name cannot be longer than {maxLength} chars");
}
