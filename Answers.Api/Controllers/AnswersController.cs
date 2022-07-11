using Answers.Api.Models.Request;
using Answers.Api.Models.Request.Surveys;
using Answers.Api.Models.Response.Answers;
using Answers.Api.Models.Response.Pages;
using Answers.Api.Models.Response.Results;
using Answers.Domain.Answers.Commands;
using Answers.Domain.Answers.Queries;
using Answers.Domain.Personalities.Commands;
using Answers.Models.Answers;
using Answers.Models.Personality;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SurveyMe.Common.Exceptions;
using SurveyMe.Error.Models.Response;

namespace Answers.Api.Controllers;


/// <summary>
/// Controller for interaction with answers
/// </summary>
[ApiController]
[Route("api/surveys/{surveyId:guid}/[controller]")]
[Authorize]
public sealed class AnswersController : Controller
{
    private readonly IMapper _mapper;

    private readonly IMediator _mediator;
    
    
    /// <summary>
    /// Constructor of the controller
    /// </summary>
    /// <param name="mapper">Automapper instance</param>
    /// <param name="mediator">Mediator instance</param>
    public AnswersController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    
    /// <summary>
    /// Endpoint for adding answer
    /// </summary>
    /// <param name="surveyAnswerRequestModel">Model for creating request</param>
    /// <param name="surveyId">Survey id</param>
    /// <returns>Created survey</returns>
    /// <exception cref="BadRequestException">
    /// If survey id in answer model do not match with route
    /// and model is invalid 
    /// </exception>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpPost]
    public async Task<IActionResult> AddAnswer([FromBody]SurveyAnswerRequestModel surveyAnswerRequestModel,
        Guid surveyId)
    {
        if (surveyAnswerRequestModel.SurveyId != surveyId)
        {
            throw new BadRequestException("Route id and request id do not match");
        }
        
        if (!ModelState.IsValid)
        {
            var errors = ModelState.ToDictionary(
                error => error.Key,
                error => error.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
            );
            
            throw new BadRequestException("Invalid data", errors);
        }

        var personality = _mapper.Map<Personality>(surveyAnswerRequestModel.Personality);

        var personalityAddCommand = new AddPersonalityCommand(personality, surveyId);

        var personalityId = await _mediator.Send(personalityAddCommand);

        var answer = _mapper.Map<SurveyAnswer>(surveyAnswerRequestModel);

        answer.PersonalityId = personalityId;
        
        var command = new AddAnswerCommand(answer);
        answer = await _mediator.Send(command);
        var answerResponse = _mapper.Map<SurveyAnswerResponseModel>(answer);
        
        return CreatedAtAction(Url.Action(nameof(GetAllAnswerBySurveyId)), answerResponse);
    }

    /// <summary>
    /// Endpoint for getting all answers by survey id
    /// </summary>
    /// <param name="surveyId"></param>
    /// <returns>Array of survey answers</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpGet]
    public async Task<IActionResult> GetAllAnswerBySurveyId(Guid surveyId)
    {
        var query = new GetAllAnswersBySurveyIdQuery(surveyId);
        var answers = await _mediator.Send(query);

        var answersResponse = _mapper.Map<IReadOnlyCollection<SurveyAnswerResponseModel>>(answers);
        
        return Ok(answersResponse);
    }

    /// <summary>
    /// Endpoint for getting page with surveys 
    /// </summary>
    /// <param name="request">Model for request</param>
    /// <param name="surveyId">Survey id</param>
    /// <param name="page">Page number</param>
    /// <returns>Paginated result</returns>
    /// <exception cref="BadRequestException">If request is null</exception>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseErrorResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseErrorResponse))]
    [HttpGet("{page:int}")]
    public async Task<IActionResult> GetAnswers([FromQuery] GetPageRequest request, Guid surveyId, int page = 1)
    {
        if (request == null)
        {
            throw new BadRequestException("Request is empty");
        }

        var query = new GetSurveyAnswersPageQuery(page, request.PageSize, surveyId);
        
        var answers = await _mediator.Send(query);
        var response = _mapper.Map<PagedResultResponseModel<SurveyAnswerResultResponseModel>>(answers);

        return Ok(response);
    }
}