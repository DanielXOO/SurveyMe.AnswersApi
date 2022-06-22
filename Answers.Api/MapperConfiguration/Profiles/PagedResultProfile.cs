using Answers.Api.Models.Response.Pages;
using Answers.Api.Models.Response.Results;
using Answers.Domain.Answers.Models.Surveys;
using AutoMapper;
using SurveyMe.Common.Pagination;

namespace Answers.Api.MapperConfiguration.Profiles;

public sealed class PagedResultProfile : Profile
{
    public PagedResultProfile()
    {
        CreateMap<PagedResult<SurveyAnswerResult>, PagedResultResponseModel<SurveyAnswerResultResponseModel>>();
    }
}