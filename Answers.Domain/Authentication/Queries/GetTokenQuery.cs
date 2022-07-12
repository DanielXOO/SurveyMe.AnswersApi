using MediatR;
using SurveyMe.AuthenticationApi.Models.Request;

namespace Answers.Domain.Authentication.Queries;

public class GetTokenQuery : IRequest<string>
{
    public GetTokenRequestModel TokenRequest { get; set; }
    
    
    public GetTokenQuery(GetTokenRequestModel tokenRequest)
    {
        TokenRequest = tokenRequest;
    }
}