using Answers.Data.Abstracts;
using Answers.Domain.Answers.Queries;
using Answers.Models.Answers;
using MediatR;

namespace Answers.Domain.Answers.QueriesHandlers;

public class GetAnswerByIdQueryHandler : IRequestHandler<GetAnswerByIdQuery, SurveyAnswer>
{
    private readonly IAnswersUnitOfWork _unitOfWork;
    
    
    public GetAnswerByIdQueryHandler(IAnswersUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    
    public async Task<SurveyAnswer> Handle(GetAnswerByIdQuery request, CancellationToken cancellationToken)
    {
        var answer = await _unitOfWork.Answers.GetByIdAsync(request.Id);

        return answer;
    }
}