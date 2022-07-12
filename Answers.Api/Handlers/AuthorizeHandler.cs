using System.Net.Http.Headers;
using Answers.Domain.Authentication.Queries;
using MediatR;
using SurveyMe.AuthenticationApi.Models.Request;
using SurveyMe.Common.Exceptions;

namespace Answers.Api.Handlers;

public class AuthorizeHandler : DelegatingHandler
{
    private readonly IMediator _mediator;
    
    
    public AuthorizeHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var requestToken = new GetTokenRequestModel
        {
            client_id = "api",
            client_secret = "api_secret",
            grant_type = "client_credentials",
            scope = "ApisScope"
        };

        var command = new GetTokenQuery(requestToken);

        var token = await _mediator.Send(command, cancellationToken);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        return await base.SendAsync(request, cancellationToken);
    }
}