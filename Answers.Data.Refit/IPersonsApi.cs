using Refit;
using SurveyMe.PersonsApi.Models.Request.Personality;
using SurveyMe.PersonsApi.Models.Response.Personality;

namespace Answers.Data.Refit;

public interface IPersonsApi
{
    [Post("/api/persons")]
    Task<PersonalityResponseModel> AddPersonalityAsync(PersonalityCreateRequestModel personalityRequest);
}