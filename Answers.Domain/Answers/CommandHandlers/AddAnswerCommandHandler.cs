using Answers.Data.Abstracts;
using Answers.Data.Refit;
using Answers.Domain.Answers.Commands;
using AutoMapper;
using MediatR;
using SurveyMe.Common.Exceptions;

namespace Answers.Domain.Answers.CommandHandlers;

public class AddAnswerCommandHandler : IRequestHandler<AddAnswerCommand>
{
    private readonly IAnswersUnitOfWork _unitOfWork;
    
    private readonly ISurveyPersonApi _surveyPersonApi;

    private readonly IPersonsApi _personsApi;

    private readonly IMapper _mapper;
    
    
    public AddAnswerCommandHandler(IAnswersUnitOfWork unitOfWork, ISurveyPersonApi surveyPersonApi,
        IMapper mapper, IPersonsApi personsApi)
    {
        _unitOfWork = unitOfWork;
        _surveyPersonApi = surveyPersonApi;
        _mapper = mapper;
        _personsApi = personsApi;
    }
    
    public async Task<Unit> Handle(AddAnswerCommand request, CancellationToken cancellationToken)
    {
        var survey = await _unitOfWork.Surveys.GetByIdAsync(request.Answer.SurveyId);

        if (survey == null)
        {
            throw new NotFoundException("Survey not found");
        }
        
        await _unitOfWork.Answers.CreateAsync(request.Answer);
        
        return Unit.Value;
    }
}