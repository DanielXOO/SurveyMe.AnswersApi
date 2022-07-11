using Answers.Data.Abstracts;
using Answers.Domain.Answers.Models.Questions;
using Answers.Domain.Answers.Models.Surveys;
using Answers.Domain.Answers.Queries;
using Answers.Models.Answers;
using MediatR;
using SurveyMe.Common.Pagination;

namespace Answers.Domain.Answers.QueriesHandlers;

public class GetSurveyAnswersQueryHandler : IRequestHandler<GetSurveyAnswersPageQuery, PagedResult<SurveyAnswerResult>>
{
    private readonly IAnswersUnitOfWork _unitOfWork;
    
    
    public GetSurveyAnswersQueryHandler(IAnswersUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    
    public async Task<PagedResult<SurveyAnswerResult>> Handle(GetSurveyAnswersPageQuery request, 
        CancellationToken cancellationToken)
    {
        var answers = await _unitOfWork.Answers
            .GetSurveyAnswersAsync(request.CurrentPage, request.PageSize, request.SurveyId);

        var survey = await _unitOfWork.Surveys.GetByIdAsync(request.SurveyId);

        var result = new List<SurveyAnswerResult>();
        
        foreach (var answer in answers.Items)
        {
            var surveyAnswer = new SurveyAnswerResult
            {
                Name = survey.Name,
                PersonalityId = answer.PersonalityId,
                QuestionAnswers = answer.QuestionsAnswers.Join(survey.Questions,
                    questionAnswer => questionAnswer.QuestionId,
                    question => question.Id,
                    (questionAnswer, question) =>
                    {
                        BaseAnswerResult answerResult = questionAnswer switch
                        {
                            TextQuestionAnswer textAnswer => new TextAnswerResult
                            {
                                Title = question.Title,
                                TextAnswer = textAnswer.Text,
                                QuestionType = questionAnswer.QuestionType
                            },
                            CheckboxQuestionAnswer checkboxAnswer => new CheckboxAnswerResult
                            {
                                Title = question.Title,
                                Options = checkboxAnswer.Options.Join(question.Options,
                                    optionAnswer => optionAnswer.OptionId,
                                    questionOption => questionOption.Id,
                                    (_, questionOption) => questionOption.Text)
                                    .ToArray(),
                                QuestionType = questionAnswer.QuestionType
                            },
                            RadioQuestionAnswer radioAnswer => new RadioAnswerResult
                            {
                                Title = question.Title,
                                RadioAnswer = question.Options
                                    .First(option => option.Id == radioAnswer.OptionId)
                                    .Text,
                                QuestionType = questionAnswer.QuestionType
                            },
                            RateQuestionAnswer rateAnswer => new RateAnswerResult
                            {
                                Title = question.Title,
                                Rate = rateAnswer.Rate,
                                QuestionType = questionAnswer.QuestionType
                            },
                            ScaleQuestionAnswer scaleAnswer => new ScaleAnswerResult
                            {
                                Title = question.Title,
                                Scale = scaleAnswer.Scale,
                                QuestionType = questionAnswer.QuestionType
                            },
                            FileQuestionAnswer fileAnswer => new FileAnswerResult
                            {
                                QuestionType = questionAnswer.QuestionType
                            },
                            _ => throw new ArgumentOutOfRangeException(nameof(questionAnswer),
                                questionAnswer, "No such answer type")
                        };

                        return answerResult;
                    }
                ).ToList()
            };
            
            result.Add(surveyAnswer);
        }

        var resultPage = new PagedResult<SurveyAnswerResult>(result, request.PageSize,
            request.CurrentPage, result.Count);
        
        return resultPage;
    }
}