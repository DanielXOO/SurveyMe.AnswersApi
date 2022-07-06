using Answers.Models.Answers;
using Answers.Models.Options;
using Answers.Models.Questions;
using Answers.Models.Surveys;
using AutoMapper;
using SurveyMe.AnswersApi.Models.Queue;
using SurveyMe.QueueModels;

namespace Answers.Domain.Answers.AutoMapper.Profiles;

public class QueueModelsProfile : Profile
{
    public QueueModelsProfile()
    {
        CreateMap<SurveyQueueModel, Survey>();
        CreateMap<SurveyAnswer, SurveyAnswerQueue>()
            .ForMember(dest => dest.Answers,
                opt => 
                    opt.MapFrom(src => src.QuestionsAnswers));
        CreateMap<QuestionQueueModel, Question>();
        CreateMap<OptionQueueModel, Option>();

        CreateMap<BaseQuestionAnswerQueue, BaseQuestionAnswer>()
            .Include<TextQuestionAnswerQueue, TextQuestionAnswer>()
            .Include<FileQuestionAnswerQueue, FileQuestionAnswer>()
            .Include<RateQuestionAnswerQueue, RateQuestionAnswer>()
            .Include<ScaleQuestionAnswerQueue, ScaleQuestionAnswer>()
            .Include<RadioQuestionAnswerQueue, RadioQuestionAnswer>()
            .Include<CheckboxQuestionAnswerQueue, CheckboxQuestionAnswer>()
            .ReverseMap();

        CreateMap<TextQuestionAnswerQueue, TextQuestionAnswer>().ReverseMap();
        CreateMap<FileQuestionAnswerQueue, FileQuestionAnswer>().ReverseMap();
        CreateMap<RateQuestionAnswerQueue, RateQuestionAnswer>().ReverseMap();
        CreateMap<ScaleQuestionAnswerQueue, ScaleQuestionAnswer>().ReverseMap();
        CreateMap<RadioQuestionAnswerQueue, RadioQuestionAnswer>().ReverseMap();
        CreateMap<CheckboxQuestionAnswerQueue, CheckboxQuestionAnswer>().ReverseMap();
        
        CreateMap<OptionQuestionAnswerQueue, OptionQuestionAnswer>().ReverseMap();
    }
}