using Answers.Models.Answers;
using MediatR;

namespace Answers.Domain.Answers.Commands;

public class AddAnswerCommand : IRequest
{
    public SurveyAnswer Answer { get; }
    
    
    public AddAnswerCommand(SurveyAnswer answer)
    {
        Answer = answer;
    }
}