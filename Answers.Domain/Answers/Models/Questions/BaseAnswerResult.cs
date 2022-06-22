using Answers.Models.Questions;

namespace Answers.Domain.Answers.Models.Questions;

public class BaseAnswerResult
{
    public string Title { get; set; }
    
    public QuestionType QuestionType { get; set; }
}