using Answers.Models.Answers;
using MediatR;

namespace Answers.Domain.Answers.Commands;

public class DeleteAnswerCommand : IRequest
{ 
    public SurveyAnswer Answer { get; }
    
    
    public DeleteAnswerCommand(SurveyAnswer answer)
    {
        Answer = answer;
    }
}