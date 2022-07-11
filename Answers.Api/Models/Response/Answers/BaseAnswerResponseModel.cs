using Answers.Models.Questions;

namespace Answers.Api.Models.Response.Answers;

public abstract class BaseAnswerResponseModel
{
    public Guid Id { get; set; }
    
    public QuestionType QuestionType { get; set; }
    
    public Guid QuestionId { get; set; }
}