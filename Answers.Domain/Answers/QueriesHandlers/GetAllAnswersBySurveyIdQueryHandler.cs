using Answers.Data.Abstracts;
using Answers.Domain.Answers.Queries;
using Answers.Models.Answers;
using MediatR;

namespace Answers.Domain.Answers.QueriesHandlers;

public class GetAllAnswersBySurveyIdQueryHandler
    : IRequestHandler<GetAllAnswersBySurveyIdQuery, IReadOnlyCollection<SurveyAnswer>>
{
    private readonly IAnswersUnitOfWork _unitOfWork;


    public GetAllAnswersBySurveyIdQueryHandler(IAnswersUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyCollection<SurveyAnswer>> Handle(GetAllAnswersBySurveyIdQuery request, 
        CancellationToken cancellationToken)
    {
        var answers = await _unitOfWork.Answers.GetAllAnswersBySurveyId(request.Id);

        return answers;
    }
}