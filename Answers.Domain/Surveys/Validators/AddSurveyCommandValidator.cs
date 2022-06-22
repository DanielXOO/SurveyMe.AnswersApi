using Answers.Domain.Surveys.Commands;
using FluentValidation;

namespace Answers.Domain.Surveys.Validators;

public class AddSurveyCommandValidator : AbstractValidator<AddSurveyCommand>
{
    public AddSurveyCommandValidator()
    {
        RuleFor(x => x.Survey).NotNull();
        RuleFor(x => x.Survey.Id).NotEmpty();
        RuleFor(x => x.Survey.Questions).NotNull();
        RuleFor(x => x.Survey.Name).MinimumLength(1);
        RuleFor(x => x.Survey.Id).NotEmpty();
    }
}