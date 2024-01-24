namespace Platform.Domain.Common.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string code, string description, DomainExceptionKind kind)
    {
        Code = code;
        Description = description;
        Kind = kind;
    }

    public string Code { get; }

    public string Description { get; }

    public DomainExceptionKind Kind { get; }

    public override string Message => $"{Code}: {Description}";
}
