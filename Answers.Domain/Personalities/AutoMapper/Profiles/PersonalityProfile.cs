using Answers.Models.Personality;
using Answers.Models.SurveysOptions;
using AutoMapper;
using SurveyMe.PersonsApi.Models.Request.Personality;
using SurveyMe.SurveyPersonApi.Models.Response.Options.Personality;
using SurveyMe.SurveyPersonApi.Models.Response.Options.Survey;

namespace Answers.Domain.Personalities.AutoMapper.Profiles;

public class PersonalityProfile : Profile
{
    public PersonalityProfile()
    {
        CreateMap<Personality, PersonalityCreateRequestModel>();
        CreateMap<SurveyOptionsResponseModel, SurveyPersonOptions>();
        CreateMap<PersonalityOptionResponseModel, PersonalityOption>();
    }
}