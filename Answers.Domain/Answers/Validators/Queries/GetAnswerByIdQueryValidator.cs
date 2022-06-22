using Answers.Domain.Answers.Queries;
using FluentValidation;

namespace Answers.Domain.Answers.Validators.Queries;

public class GetAnswerByIdQueryValidator : AbstractValidator<GetAnswerByIdQuery>
{
    public GetAnswerByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}