namespace Platform.Domain.Common;

public record Result
{
    private readonly Error? _error;

    internal Result()
    {
        IsSuccess = true;
        _error = default;
    }

    internal Result(Error error)
    {
        IsSuccess = false;
        _error = error;
    }

    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;

    public Error Error
    {
        get
        {
            if (IsSuccess)
            {
                throw new InvalidOperationException($"Unable obtain {nameof(Error)} property for successful result");
            }

            return _error!;
        }
    }

    public static Result Success() => new();

    public static Result<T> Success<T>(T value) => new(value);

    public static Result Failure(Error error) => new(error);

    public static Result<T> Failure<T>(Error error) => new(error);

    public TResult Match<TResult>(Func<TResult> onSuccess, Func<Error, TResult> onFailure)
    {
        ArgumentNullException.ThrowIfNull(onSuccess);
        ArgumentNullException.ThrowIfNull(onFailure);

        return IsSuccess ? onSuccess() : onFailure(Error!);
    }
}

public record Result<T> : Result
{
    private readonly T? _value;

    internal Result(T value)
    {
        _value = value;
    }

    internal Result(Error error) : base(error)
    {
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

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<Error, TResult> onFailure)
    {
        ArgumentNullException.ThrowIfNull(onSuccess);
        ArgumentNullException.ThrowIfNull(onFailure);

        return IsSuccess ? onSuccess(Value) : onFailure(Error!);
    }
}
