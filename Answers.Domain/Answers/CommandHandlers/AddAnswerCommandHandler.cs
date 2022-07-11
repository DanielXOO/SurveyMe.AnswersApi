using Answers.Data.Abstracts;
using Answers.Data.Refit;
using Answers.Domain.Answers.Commands;
using Answers.Models.Answers;
using AutoMapper;
using MassTransit;
using MediatR;
using SurveyMe.AnswersApi.Models.Queue;
using SurveyMe.Common.Exceptions;

namespace Answers.Domain.Answers.CommandHandlers;

public class AddAnswerCommandHandler : IRequestHandler<AddAnswerCommand, SurveyAnswer>
{
    private readonly IAnswersUnitOfWork _unitOfWork;

    private readonly IBus _bus;

    private readonly IMapper _mapper;
    
    
    public AddAnswerCommandHandler(IAnswersUnitOfWork unitOfWork, IMapper mapper, IBus bus)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _bus = bus;
    }
    
    public async Task<SurveyAnswer> Handle(AddAnswerCommand request, CancellationToken cancellationToken)
    {
        var survey = await _unitOfWork.Surveys.GetByIdAsync(request.Answer.SurveyId);

        if (survey == null)
        {
            throw new NotFoundException("Survey not found");
        }

        await _unitOfWork.Answers.CreateAsync(request.Answer);

        var answerQueue = _mapper.Map<SurveyAnswerQueue>(request.Answer);

        await _bus.Publish(answerQueue, cancellationToken);
        
        return request.Answer;
    }
}