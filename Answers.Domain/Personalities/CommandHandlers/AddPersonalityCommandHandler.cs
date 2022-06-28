using Answers.Data.Abstracts;
using Answers.Data.Refit;
using Answers.Domain.Personalities.Commands;
using AutoMapper;
using MediatR;
using SurveyMe.Common.Exceptions;
using SurveyMe.PersonsApi.Models.Request.Personality;

namespace Answers.Domain.Personalities.CommandHandlers;

public class AddPersonalityCommandHandler : IRequestHandler<AddPersonalityCommand>
{
    private readonly IAnswersUnitOfWork _unitOfWork;

    private readonly ISurveyPersonApi _surveyPersonApi;

    private readonly IPersonsApi _personsApi;

    private readonly IMapper _mapper;
    
    
    public AddPersonalityCommandHandler(IAnswersUnitOfWork unitOfWork, ISurveyPersonApi surveyPersonApi,
        IPersonsApi personsApi, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _surveyPersonApi = surveyPersonApi;
        _personsApi = personsApi;
        _mapper = mapper;
    }
    
    
    public async Task<Unit> Handle(AddPersonalityCommand request, CancellationToken cancellationToken)
    {
        var survey = await _unitOfWork.Surveys.GetByIdAsync(request.SurveyId);
        var options = await _surveyPersonApi.GetSurveyPersonOptionsAsync(survey.SurveyOptionId);
        var personality = request.Personality;

        var errors = new Dictionary<string, string[]>();
        
        foreach (var personalityOption in options.Options)
        {
            if (!personalityOption.IsRequired)
            {
                continue;
            }

            var isValid = personality.GetType().GetProperty(personalityOption.PropertyName)?
                .GetValue(personality);

            if (isValid == null)
            {
                errors.Add(personalityOption.PropertyName, new [] { "Can not be empty" });
            }
        }

        if (errors.Any())
        {
            throw new BadRequestException("Validation error", errors);
        }

        var personalityRequest = _mapper.Map<PersonalityCreateRequestModels>(personality);

        await _personsApi.AddPersonalityAsync(personalityRequest);
        
        return Unit.Value;
    }
}