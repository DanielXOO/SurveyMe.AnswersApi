using Answers.Domain.Surveys.Commands;
using Answers.Models.Surveys;
using AutoMapper;
using MassTransit;
using MediatR;
using SurveyMe.QueueModels;
using ILogger = SurveyMe.Common.Logging.Abstracts.ILogger;

namespace Answers.Api.Consumers;

public sealed class SurveysConsumer : IConsumer<SurveyQueueModel>
{
    private readonly ILogger _logger;

    private readonly IMapper _mapper;

    private readonly IMediator _mediator;

    
    public SurveysConsumer(ILogger logger, IMapper mapper, IMediator mediator)
    {
        _logger = logger;
        _mapper = mapper;
        _mediator = mediator;
    }

    
    public async Task Consume(ConsumeContext<SurveyQueueModel> context)
    {
        var survey = context.Message;
        
        _logger.LogInformation($"SurveyApi send action {survey.EventType}");
        
        await HandleEventAsync(survey);
    }


    private async Task HandleEventAsync(SurveyQueueModel surveyQueue)
    {
        var survey = _mapper.Map<Survey>(surveyQueue);
        
        switch (surveyQueue.EventType)
        {
            case EventType.Create:
                var createCommand = new AddSurveyCommand(survey);
                await _mediator.Send(createCommand);
                break;
            case EventType.Update:
                var updateCommand = new AddSurveyCommand(survey);
                await _mediator.Send(updateCommand);
                break;
            case EventType.Delete:
                var deleteCommand = new AddSurveyCommand(survey);
                await _mediator.Send(deleteCommand);
                break;
            default:
                throw new 
                    ArgumentOutOfRangeException(nameof(surveyQueue), surveyQueue.EventType, "No such event");
        }
    }
}