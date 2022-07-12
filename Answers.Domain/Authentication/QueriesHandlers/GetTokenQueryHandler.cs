using Answers.Data.Refit;
using Answers.Domain.Authentication.Queries;
using MediatR;
using SurveyMe.Common.Exceptions;

namespace Answers.Domain.Authentication.QueriesHandlers;

public class GetTokenQueryHandler : IRequestHandler<GetTokenQuery, string>
{
    private readonly IAuthenticationApi _authenticationApi;
    
    
    public GetTokenQueryHandler(IAuthenticationApi authenticationApi)
    {
        _authenticationApi = authenticationApi;
    }
    
    
    public async Task<string> Handle(GetTokenQuery request, CancellationToken cancellationToken)
    {
        var token = await _authenticationApi.GetTokenAsync(request.TokenRequest);

        if (token == null)
        {
            throw new BadRequestException("Token response is empty");
        }

        return token.access_token;
    }
}