using Refit;
using SurveyMe.PersonsApi.Models.Request.Personality;
using SurveyMe.PersonsApi.Models.Response.Personality;

namespace Answers.Data.Refit;

public interface IPersonsApi
{
    [Get("/api/persons/{id}")]
    Task<PersonalityResponseModel> GetPersonalityAsync(string id);

    [Post("/api/persons")]
    Task<Guid> AddPersonalityAsync(PersonalityCreateRequestModels personalityRequest);
}