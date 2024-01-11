using System.Diagnostics.CodeAnalysis;

namespace Platform.Shared;

public sealed record Result : ResultBase
{
    private Result() : base(true, null)
    {
    }

    private Result(Error error) : base(false, error)
    {
    }

    public static Result Success() => new();

    public static Result<T> Success<T>(T value) => new(value);

    public static Result Failure(Error error) => new(error);

    public static Result<T> Failure<T>(Error error) => new(error);

    public TResult Match<TResult>([NotNull] Func<TResult> onSuccess, [NotNull] Func<Error, TResult> onFailure)
    {
        return IsSuccess ? onSuccess() : onFailure(Error);
    }
}
