namespace Platform.Application.Common.Behaviours;

public sealed class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IValidationRequest
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
            var failures = await ValidationAsync(request, cancellationToken);

            if (failures.Any())
            {
                throw new ValidationFailedException(failures);
            }
        }

        return await next();
    }

    private bool HasValidators()
    {
        return _validators.Any();
    }

    private async Task<ValidationFailureRecord[]> ValidationAsync(TRequest request, CancellationToken cancellationToken)
    {
        var tasks = _validators.Select(v => v.ValidateAsync(request, cancellationToken));
        var validationResults = await Task.WhenAll(tasks);

        var failures = validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .Where(f => f is not null)
            .Select(f => new ValidationFailureRecord(f.PropertyName, f.ErrorMessage))
            .ToArray();

        return failures;
    }
}
