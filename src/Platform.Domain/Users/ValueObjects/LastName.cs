﻿using Platform.Domain.Users.Exceptions;

namespace Platform.Domain.Users.ValueObjects;

public sealed class LastName : ValueObject
{
    public const int MaxLength = 100;

    private LastName(string value)
    {
        Value = value;
    }

    public string Value { get; }

    public static LastName Create(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new LastNameEmptyException();
        }

        var processedValue = value.Trim();

        if (processedValue.Length > MaxLength)
        {
            throw new LastNameTooLongException(MaxLength);
        }

        return new LastName(processedValue);
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
