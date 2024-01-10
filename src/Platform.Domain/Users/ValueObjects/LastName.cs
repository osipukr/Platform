using Platform.Domain.Common;

namespace Platform.Domain.Users.ValueObjects;

public sealed class LastName : ValueObject
{
    public const int MaxLength = 100;

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

    public override string ToString()
    {
        return Value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
