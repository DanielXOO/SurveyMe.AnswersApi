using Answers.Models.Questions;

namespace Answers.Api.Models.Request.Answers;

public abstract class BaseAnswerRequestModel
{
    /// <summary>
    /// <value>Question type</value>
    /// <example>
    /// Text, Radio, Checkbox, File, Rate, Scale
    /// </example>
    /// </summary>
    public QuestionType QuestionType { get; set; }
    
    /// <summary>
    /// <value>Question id</value>
    /// </summary>
    public Guid QuestionId { get; set; }
}