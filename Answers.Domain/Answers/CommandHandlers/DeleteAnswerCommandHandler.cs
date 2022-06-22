using Answers.Data.Abstracts;
using Answers.Domain.Answers.Commands;
using MediatR;

namespace Answers.Domain.Answers.CommandHandlers;

public class DeleteAnswerCommandHandler : IRequestHandler<DeleteAnswerCommand>
{
    private readonly IAnswersUnitOfWork _unitOfWork;
    
    
    public DeleteAnswerCommandHandler(IAnswersUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    
    public async Task<Unit> Handle(DeleteAnswerCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Answers.DeleteAsync(request.Answer);
        
        return Unit.Value;
    }
}