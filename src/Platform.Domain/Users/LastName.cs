using Platform.Domain.Common;

namespace Platform.Domain.Users;

public sealed class LastName : ValueObject
{
    private const int MaxLength = 100;

    private LastName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<LastName> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<LastName>(UserErrors.LastNameEmpty);
        }

        var processedValue = value.Trim();

        if (processedValue.Length > MaxLength)
        {
            return Result.Failure<LastName>(UserErrors.LastNameTooLong(MaxLength));
        }

        return Result.Success(new LastName(processedValue));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
