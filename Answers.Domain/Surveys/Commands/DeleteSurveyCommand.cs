using Answers.Models.Surveys;
using MediatR;

namespace Answers.Domain.Surveys.Commands;

public class DeleteSurveyCommand : IRequest
{
    public Survey Survey { get; }


    public DeleteSurveyCommand(Survey survey)
    {
        Survey = survey;
    }
}