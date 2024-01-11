﻿using System.Diagnostics.CodeAnalysis;

namespace Platform.Shared;

public record Result : ResultBase
{
    protected Result() : base(true, null)
    {
    }

    protected Result(Error error) : base(false, error)
    {
    }

    public static Result Success() => new();

    public static Result<T> Success<T>(T value) => Result<T>.Success(value);

    public static Result Failure(Error error) => new(error);

    public static Result<T> Failure<T>(Error error) => Result<T>.Failure(error);

    public TResult Match<TResult>([NotNull] Func<TResult> onSuccess, [NotNull] Func<Error, TResult> onFailure)
    {
        return IsSuccess ? onSuccess() : onFailure(Error);
    }
}
