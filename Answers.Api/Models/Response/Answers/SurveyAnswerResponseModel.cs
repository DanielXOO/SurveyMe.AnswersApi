namespace Answers.Api.Models.Response.Answers;

public sealed class SurveyAnswerResponseModel
{
    public Guid Id { get; set; }
    
    public Guid SurveyId { get; set; }
        
    public Guid PersonalityId { get; set; }
    
    public ICollection<BaseAnswerResponseModel> QuestionsAnswers { get; set; }
}