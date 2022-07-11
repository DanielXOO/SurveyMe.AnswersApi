using Answers.Domain.Answers.Models.Questions;

namespace Answers.Domain.Answers.Models.Surveys;

public class SurveyAnswerResult
{
    public string Name { get; set; }

    public Guid PersonalityId { get; set; }
    
    public ICollection<BaseAnswerResult> QuestionAnswers { get; set; }
}