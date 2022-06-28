using Answers.Models.Answers;
using MediatR;

namespace Answers.Domain.Answers.Queries;

public class GetAllAnswersBySurveyIdQuery : IRequest<IReadOnlyCollection<SurveyAnswer>>
{
    public Guid Id { get; }

    
    public GetAllAnswersBySurveyIdQuery(Guid id)
    {
        Id = id;
    }
}