using FluentValidation;
using MediatR;
using SurveyMe.Common.Exceptions;

namespace Answers.Domain.Validation;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;


    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    
    
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, 
        RequestHandlerDelegate<TResponse> next)
    {
        if (!_validators.Any())
        {
            return await next();
        }
        
        var results = _validators.Select(v => v.Validate(request));

        var hasError = results.Any(e => !e.IsValid);

        if (!hasError)
        {
            return await next();
        }
        
        var errorMessages = results.SelectMany(r => r.Errors)
            .GroupBy(r => r.PropertyName, r => r.ErrorMessage,
                (key, value) 
                    => new KeyValuePair<string, IEnumerable<string>>(key, value))
            .ToDictionary(e => e.Key, 
                e => e.Value.ToArray());
        
        throw new BadRequestException("Invalid model", errorMessages);
    }
}