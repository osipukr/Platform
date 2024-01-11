using System.Diagnostics.CodeAnalysis;

namespace Platform.Shared;

public record Result<T> : ResultBase
{
    private readonly T? _value;

    protected Result(T value) : base(true, null)
    {
        _value = value;
    }

    protected Result(Error error) : base(false, error)
    {
        _value = default;
    }

    public T Value
    {
        get
        {
            if (IsFailure)
            {
                throw new InvalidOperationException($"Unable obtain {nameof(Value)} property for failed result");
            }

            return _value!;
        }
    }

    public TResult Match<TResult>([NotNull] Func<T, TResult> onSuccess, [NotNull] Func<Error, TResult> onFailure)
    {
        return IsSuccess ? onSuccess(Value) : onFailure(Error);
    }

    public static Result<T> Success(T value) => new(value);

    public static Result<T> Failure(Error error) => new(error);
}
