using Answers.Api.Models.Request.Answers;
using SurveyMe.PersonsApi.Models.Request.Personality;

namespace Answers.Api.Models.Request.Surveys;

/// <summary>
/// Model for survey creating
/// </summary>
public sealed class SurveyAnswerRequestModel
{
    /// <summary>
    /// <value>Survey id</value>
    /// </summary>
    public Guid SurveyId { get; set; }
        
    /// <summary>
    /// <value>Personality info model</value>
    /// </summary>
    public PersonalityCreateRequestModel Personality { get; set; }
    
    /// <summary>
    /// <value>Answers for questions</value>
    /// </summary>
    public ICollection<BaseAnswerRequestModel> QuestionsAnswers { get; set; }
}