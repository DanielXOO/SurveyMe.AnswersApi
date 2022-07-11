using System.Text.Json;
using Answers.Data.Abstracts;
using Answers.Data.Refit;
using Answers.Domain.Personalities.Commands;
using Answers.Models.SurveysOptions;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using SurveyMe.Common.Exceptions;
using SurveyMe.PersonsApi.Models.Request.Personality;
using SurveyMe.SurveyPersonApi.Models.Response.Options.Survey;

namespace Answers.Domain.Personalities.CommandHandlers;

public class AddPersonalityCommandHandler : IRequestHandler<AddPersonalityCommand, Guid>
{
    private readonly IAnswersUnitOfWork _unitOfWork;

    private readonly ISurveyPersonOptionsApi _surveyPersonOptionsApi;

    private readonly IPersonsApi _personsApi;

    private readonly IMapper _mapper;

    private readonly IDistributedCache _cache;
    
    
    public AddPersonalityCommandHandler(IAnswersUnitOfWork unitOfWork, ISurveyPersonOptionsApi surveyPersonOptionsApi,
        IPersonsApi personsApi, IMapper mapper, IDistributedCache cache)
    {
        _unitOfWork = unitOfWork;
        _surveyPersonOptionsApi = surveyPersonOptionsApi;
        _personsApi = personsApi;
        _mapper = mapper;
        _cache = cache;
    }
    
    
    public async Task<Guid> Handle(AddPersonalityCommand request, CancellationToken cancellationToken)
    {
        var survey = await _unitOfWork.Surveys.GetByIdAsync(request.SurveyId);

        var serializedOptions = await _cache.GetStringAsync(survey.Id.ToString(), cancellationToken);

        SurveyOptionsResponseModel optionsResponse;
        
        if (string.IsNullOrEmpty(serializedOptions))
        {
            optionsResponse = await _surveyPersonOptionsApi.GetSurveyOptionsAsync(survey.Id);

            serializedOptions = JsonSerializer.Serialize(optionsResponse);
            
            await _cache.SetStringAsync(survey.Id.ToString(), serializedOptions, cancellationToken);
        }
        else
        {
            optionsResponse = JsonSerializer.Deserialize<SurveyOptionsResponseModel>(serializedOptions);
        }

        var options = _mapper.Map<SurveyPersonOptions>(optionsResponse);
        
        var personality = request.Personality;

        var errors = new Dictionary<string, string[]>();
        
        foreach (var personalityOption in options.Options)
        {
            if (!personalityOption.IsRequired)
            {
                continue;
            }

            var isValid = personality
                .GetType()
                .GetProperty(personalityOption.PropertyName)?
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

        var personalityRequest = _mapper.Map<PersonalityCreateRequestModel>(personality);

        var personalityResponse = await _personsApi.AddPersonalityAsync(personalityRequest);
        
        return personalityResponse.PersonalityId;
    }
}