namespace Platform.Shared;

public abstract record ResultBase
{
    private readonly Error? _error;

    protected ResultBase(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
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
}
