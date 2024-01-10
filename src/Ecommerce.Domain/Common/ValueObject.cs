namespace Ecommerce.Domain.Common;

public abstract class ValueObject : IEquatable<ValueObject>
{
    protected abstract IEnumerable<object> GetEqualityComponents();

    public bool Equals(ValueObject? other)
    {
        return other is not null && ComponentsAreEqual(other);
    }

    public override bool Equals(object? obj)
    {
        return obj is ValueObject other && ComponentsAreEqual(other);
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents().Aggregate(default(int), HashCode.Combine);
    }

    private bool ComponentsAreEqual(ValueObject other)
    {
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }
}
