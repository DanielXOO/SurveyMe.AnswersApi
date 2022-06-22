using Answers.Models.Answers;
using MediatR;

namespace Answers.Domain.Answers.Commands;

public class EditAnswerCommand : IRequest
{
    public SurveyAnswer Answer { get; }
    
    
    public EditAnswerCommand(SurveyAnswer answer)
    {
        Answer = answer;
    }

}