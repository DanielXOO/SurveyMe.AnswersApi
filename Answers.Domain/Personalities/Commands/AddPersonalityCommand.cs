using Answers.Models.Personality;
using MediatR;

namespace Answers.Domain.Personalities.Commands;

public class AddPersonalityCommand : IRequest<Guid>
{
    public Personality Personality { get; }

    public Guid SurveyId { get; }


    public AddPersonalityCommand(Personality personality, Guid surveyId)
    {
        Personality = personality;
        SurveyId = surveyId;
    }
}