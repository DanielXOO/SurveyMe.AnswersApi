using Answers.Models.Surveys;
using MediatR;

namespace Answers.Domain.Surveys.Commands;

public class AddSurveyCommand : IRequest
{
    public Survey Survey { get; }


    public AddSurveyCommand(Survey survey)
    {
        Survey = survey;
    }
}