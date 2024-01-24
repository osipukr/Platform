namespace Platform.Application.Common.Exceptions;

public sealed record ValidationFailureRecord(string PropertyName, string ErrorMessage);
