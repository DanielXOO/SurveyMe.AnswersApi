using Answers.Data.Abstracts;
using Answers.Domain.Answers.Commands;
using MediatR;

namespace Answers.Domain.Answers.CommandHandlers;

public class AddAnswerCommandHandler : IRequestHandler<AddAnswerCommand>
{
    private readonly IAnswersUnitOfWork _unitOfWork;
    
    
    public AddAnswerCommandHandler(IAnswersUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    
    public async Task<Unit> Handle(AddAnswerCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Answers.CreateAsync(request.Answer);
        
        return Unit.Value;
    }
}