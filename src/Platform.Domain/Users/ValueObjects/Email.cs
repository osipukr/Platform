namespace Platform.Domain.Users.ValueObjects;

public sealed class Email : ValueObject
{
    private Email(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static Result<Email> Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Failure<Email>(UserErrors.EmailEmpty);
        }

        var processedValue = value.Trim();

        return Result.Success(new Email(processedValue));
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
