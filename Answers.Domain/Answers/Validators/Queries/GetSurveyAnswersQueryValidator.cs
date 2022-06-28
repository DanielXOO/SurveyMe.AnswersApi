using Answers.Domain.Answers.Queries;
using FluentValidation;

namespace Answers.Domain.Answers.Validators.Queries;

public class GetSurveyAnswersQueryValidator  : AbstractValidator<GetSurveyAnswersPageQuery>
{
    public GetSurveyAnswersQueryValidator()
    {
        RuleFor(x => x.CurrentPage).GreaterThanOrEqualTo(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
        RuleFor(x => x.SurveyId).NotEmpty();
    }
}