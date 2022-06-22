using Answers.Data.Abstracts;
using Answers.Domain.Answers.Commands;
using MediatR;

namespace Answers.Domain.Answers.CommandHandlers;

public class EditAnswerCommandHandler : IRequestHandler<EditAnswerCommand>
{
    private readonly IAnswersUnitOfWork _unitOfWork;
    

    public EditAnswerCommandHandler(IAnswersUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    
    public async Task<Unit> Handle(EditAnswerCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Answers.UpdateAsync(request.Answer);
        
        return Unit.Value;
    }
}