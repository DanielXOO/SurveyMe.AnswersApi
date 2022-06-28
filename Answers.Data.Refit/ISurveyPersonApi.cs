using Refit;
using SurveyMe.SurveyPersonApi.Models.Response.Options.Survey;

namespace Answers.Data.Refit;

public interface ISurveyPersonApi
{
    [Get("/api/surveyperson/{id}")]
    Task<SurveyOptionsResponseModel> GetSurveyPersonOptionsAsync(Guid id);
}