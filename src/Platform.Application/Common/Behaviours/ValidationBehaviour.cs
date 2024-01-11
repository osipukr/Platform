namespace Platform.Application.Common.Behaviours;

public sealed class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ResultBase
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (HasValidators())
        {
            var errors = await ValidationAsync(request, cancellationToken);

            if (errors.Any())
            {
                return CreateValidationResult(errors);
            }
        }

        return await next();
    }

    private bool HasValidators()
    {
        return _validators.Any();
    }

    private async Task<Error[]> ValidationAsync(TRequest request, CancellationToken cancellationToken)
    {
        var tasks = _validators.Select(v => v.ValidateAsync(request, cancellationToken));
        var validationResults = await Task.WhenAll(tasks);

        var errors = validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .Select(f => Error.Validation(f.PropertyName, f.ErrorMessage))
            .ToArray();

        return errors;
    }

    private static TResponse CreateValidationResult(IEnumerable<Error> errors)
    {
        if (typeof(TResponse) == typeof(Result))
        {
            return (ValidationResult.WithErrors(errors) as TResponse)!;
        }

        return typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResponse).GetGenericArguments().First())
            .GetMethod(nameof(ValidationResult<TResponse>.WithErrors))
            .Invoke(null, [errors]) as TResponse;
    }
}
