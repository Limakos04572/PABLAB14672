using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace PabLab.Application.Behaviors;

public class CommandValidationBehavior<TRequst, TResponse> : IPipelineBehavior<TRequst, TResponse>
    where TRequst : class, IRequest
{
    private readonly IEnumerable<IValidator<TRequst>> _validators;

    public CommandValidationBehavior(IEnumerable<IValidator<TRequst>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequst request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequst>(request);
        var errorList = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .GroupBy(
                x => x.PropertyName,
                x => x.ErrorMessage,
                (propertyName, errorMessage) => new ValidationFailure()
                {
                    PropertyName = propertyName,
                    ErrorMessage = string.Join(", ", errorMessage)
                })
            .ToList();

        if (errorList.Any()) 
        {
            throw new ValidationException("invalid command reasons:", errorList);
        }

        return await next();
    }
}