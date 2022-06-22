using Answers.Domain.Answers.Models.Surveys;
using MediatR;
using SurveyMe.Common.Pagination;

namespace Answers.Domain.Answers.Queries;

public class GetSurveyAnswersQuery : IRequest<PagedResult<SurveyAnswerResult>>
{
    public int CurrentPage { get; }
    
    public int PageSize { get; }
    
    public Guid SurveyId { get; }


    public GetSurveyAnswersQuery(int currentPage, int pageSize, Guid surveyId)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
        SurveyId = surveyId;
    }
}