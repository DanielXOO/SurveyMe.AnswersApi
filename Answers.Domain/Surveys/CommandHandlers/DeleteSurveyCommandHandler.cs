using Answers.Data.Abstracts;
using Answers.Domain.Surveys.Commands;
using MediatR;

namespace Answers.Domain.Surveys.CommandHandlers;

public class DeleteSurveyCommandHandler : IRequestHandler<DeleteSurveyCommand>
{
    private readonly IAnswersUnitOfWork _unitOfWork;
    
    
    public DeleteSurveyCommandHandler(IAnswersUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    
    public async Task<Unit> Handle(DeleteSurveyCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Surveys.DeleteAsync(request.Survey);
        
        return Unit.Value;
    }
}