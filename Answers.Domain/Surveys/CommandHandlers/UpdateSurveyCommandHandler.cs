using Answers.Data.Abstracts;
using Answers.Domain.Surveys.Commands;
using MediatR;

namespace Answers.Domain.Surveys.CommandHandlers;

public class UpdateSurveyCommandHandler : IRequestHandler<UpdateSurveyCommand>
{
    private readonly IAnswersUnitOfWork _unitOfWork;
    
    
    public UpdateSurveyCommandHandler(IAnswersUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    
    public async Task<Unit> Handle(UpdateSurveyCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Surveys.UpdateAsync(request.Survey);
        
        return Unit.Value;
    }
}