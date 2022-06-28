using Answers.Api.Models.Request.Answers;
using SurveyMe.PersonsApi.Models.Request.Personality;

namespace Answers.Api.Models.Request.Surveys;

public sealed class SurveyAnswerRequestModel
{
    public Guid SurveyId { get; set; }
        
    public PersonalityCreateRequestModels Personality { get; set; }
    
    public ICollection<BaseAnswerRequestModel> QuestionsAnswers { get; set; }
}