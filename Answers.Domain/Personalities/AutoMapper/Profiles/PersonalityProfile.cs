using Answers.Models.Personality;
using AutoMapper;
using SurveyMe.PersonsApi.Models.Request.Personality;

namespace Answers.Domain.Personalities.AutoMapper.Profiles;

public class PersonalityProfile : Profile
{
    public PersonalityProfile()
    {
        CreateMap<Personality, PersonalityCreateRequestModels>();
    }
}