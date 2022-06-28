using Answers.Domain.Answers.Commands;
using FluentValidation;

namespace Answers.Domain.Answers.Validators.Commands;

public class EditAnswerCommandValidator : AbstractValidator<EditAnswerCommand>
{
    public EditAnswerCommandValidator()
    {
        RuleFor(x => x.Answer).NotNull();
        RuleFor(x => x.Answer.Id).NotEmpty();
        RuleFor(x => x.Answer.SurveyId).NotEmpty();
        RuleFor(x => x.Answer.UserPersonalityId).NotEmpty();
    }
}