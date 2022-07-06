using Refit;
using SurveyMe.PersonsApi.Models.Request.Personality;

namespace Answers.Data.Refit;

public interface IPersonsApi
{
    [Post("/api/persons")]
    Task<Guid> AddPersonalityAsync(PersonalityCreateRequestModels personalityRequest);
}