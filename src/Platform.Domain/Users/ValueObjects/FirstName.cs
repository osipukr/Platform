namespace Platform.Domain.Users.ValueObjects;

public sealed class FirstName : ValueObject
{
    public const int MaxLength = 100;

    private FirstName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<FirstName> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<FirstName>(UserErrors.FirstNameEmpty);
        }

        var processedValue = value.Trim();

        if (processedValue.Length > MaxLength)
        {
            return Result.Failure<FirstName>(UserErrors.FirstNameTooLong(MaxLength));
        }

        return Result.Success(new FirstName(processedValue));
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
