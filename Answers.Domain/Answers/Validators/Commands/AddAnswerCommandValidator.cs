using Answers.Domain.Answers.Commands;
using FluentValidation;

namespace Answers.Domain.Answers.Validators.Commands;

public class AddAnswerCommandValidator : AbstractValidator<AddAnswerCommand>
{
    public AddAnswerCommandValidator()
    {
        RuleFor(x => x.Answer).NotNull();
        RuleFor(x => x.Answer.SurveyId).NotEmpty();
        RuleFor(x => x.Answer.UserPersonalityId).NotEmpty();
    }
}