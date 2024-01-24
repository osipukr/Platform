using Platform.Domain.Users.Exceptions;

namespace Platform.Domain.Users.ValueObjects;

public sealed class FirstName : ValueObject
{
    public const int MaxLength = 100;

    private FirstName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static FirstName Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new FirstNameEmptyException();
        }

        var processedValue = value.Trim();

        if (processedValue.Length > MaxLength)
        {
            throw new FirstNameTooLongException(MaxLength);
        }

        return new FirstName(processedValue);
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
