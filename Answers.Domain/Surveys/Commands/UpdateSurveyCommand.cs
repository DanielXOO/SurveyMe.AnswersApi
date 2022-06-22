using Answers.Models.Surveys;
using MediatR;

namespace Answers.Domain.Surveys.Commands;

public class UpdateSurveyCommand : IRequest
{
    public Survey Survey { get; }


    public UpdateSurveyCommand(Survey survey)
    {
        Survey = survey;
    }
}