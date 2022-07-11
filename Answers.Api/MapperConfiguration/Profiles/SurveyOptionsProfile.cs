using Answers.Models.Personality;
using AutoMapper;
using SurveyMe.PersonsApi.Models.Request.Personality;

namespace Answers.Api.MapperConfiguration.Profiles;

public class SurveyOptionsProfile : Profile
{
    public SurveyOptionsProfile()
    {
        CreateMap<PersonalityCreateRequestModel, Personality>();
    }
}