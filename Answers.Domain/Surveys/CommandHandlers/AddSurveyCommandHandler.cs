using Answers.Data.Abstracts;
using Answers.Domain.Surveys.Commands;
using MediatR;

namespace Answers.Domain.Surveys.CommandHandlers;

public class AddSurveyCommandHandler : IRequestHandler<AddSurveyCommand>
{
    private readonly IAnswersUnitOfWork _unitOfWork;
    
    
    public AddSurveyCommandHandler(IAnswersUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    
    public async Task<Unit> Handle(AddSurveyCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Surveys.CreateAsync(request.Survey);
        
        return Unit.Value;
    }
}