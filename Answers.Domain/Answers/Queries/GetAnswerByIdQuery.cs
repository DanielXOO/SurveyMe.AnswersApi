using Answers.Models.Answers;
using MediatR;

namespace Answers.Domain.Answers.Queries;

public class GetAnswerByIdQuery : IRequest<SurveyAnswer>
{
    public Guid Id { get; }
    
    
    public GetAnswerByIdQuery(Guid id)
    {
        Id = id;
    }
}