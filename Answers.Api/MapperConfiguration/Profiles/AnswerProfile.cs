using Answers.Api.Models.Request.Answers;
using Answers.Api.Models.Request.Surveys;
using Answers.Api.Models.Response.Answers;
using Answers.Api.Models.Response.Results;
using Answers.Domain.Answers.Models.Questions;
using Answers.Domain.Answers.Models.Surveys;
using Answers.Models.Answers;
using AutoMapper;
using SurveyMe.AnswersApi.Models.Queue;

namespace Answers.Api.MapperConfiguration.Profiles;

public class AnswerProfile : Profile
{
    public AnswerProfile()
    {
        CreateMap<SurveyAnswerResult, SurveyAnswerResultResponseModel>();
        CreateMap<SurveyAnswer, SurveyAnswerResponseModel>();
        CreateMap<SurveyAnswerQueue, SurveyAnswer>();

        CreateMap<BaseQuestionAnswer, BaseAnswerResponseModel>()
            .Include<TextQuestionAnswer, TextAnswerResponseModel>()
            .Include<RateQuestionAnswer, RateAnswerResponseModel>()
            .Include<ScaleQuestionAnswer, ScaleAnswerResponseModel>()
            .Include<RadioQuestionAnswer, RadioAnswerResponseModel>()
            .Include<CheckboxQuestionAnswer, CheckboxAnswerResponseModel>();

        CreateMap<TextQuestionAnswer, TextAnswerResponseModel>();
        CreateMap<RateQuestionAnswer, RateAnswerResponseModel>();
        CreateMap<ScaleQuestionAnswer, ScaleAnswerResponseModel>();
        CreateMap<RadioQuestionAnswer, RadioAnswerResponseModel>();
        CreateMap<CheckboxQuestionAnswer, CheckboxAnswerResponseModel>()
            .ForMember(dest => dest.OptionIds,
                opt => opt.MapFrom(src 
                    => src.Options.Select(options => options.CheckboxAnswerId)));
        
        CreateMap<BaseAnswerResult, BaseAnswerResultResponseModel>()
            .Include<TextAnswerResult, TextAnswerResultResponseModel>()
            .Include<RateAnswerResult, RateAnswerResultResponseModel>()
            .Include<ScaleAnswerResult, ScaleAnswerResultResponseModel>()
            .Include<RadioAnswerResult, RadioAnswerResultResponseModel>()
            .Include<CheckboxAnswerResult, CheckboxAnswerResultResponseModel>();

        CreateMap<TextAnswerResult, TextAnswerResultResponseModel>();
        CreateMap<RateAnswerResult, RateAnswerResultResponseModel>();
        CreateMap<ScaleAnswerResult, ScaleAnswerResultResponseModel>();
        CreateMap<RadioAnswerResult, RadioAnswerResultResponseModel>();
        CreateMap<CheckboxAnswerResult, CheckboxAnswerResultResponseModel>();
        
        CreateMap<SurveyAnswerRequestModel, SurveyAnswer>();
        
        CreateMap<BaseAnswerRequestModel, BaseQuestionAnswer>()
            .Include<TextAnswerRequestModel, TextQuestionAnswer>()
            .Include<FileAnswerRequestModel, FileQuestionAnswer>()
            .Include<RateAnswerRequestModel, RateQuestionAnswer>()
            .Include<ScaleAnswerRequestModel, ScaleQuestionAnswer>()
            .Include<RadioAnswerRequestModel, RadioQuestionAnswer>()
            .Include<CheckboxAnswerRequestModel, CheckboxQuestionAnswer>();

        CreateMap<TextAnswerRequestModel, TextQuestionAnswer>();

        CreateMap<TextAnswerRequestModel, TextQuestionAnswer>()
            .ForMember(dest => dest.Text, 
                opt 
                    => opt.MapFrom(src => src.TextAnswer));
        CreateMap<FileAnswerRequestModel, FileQuestionAnswer>();
        CreateMap<RateAnswerRequestModel, RateQuestionAnswer>()
            .ForMember(dest => dest.Rate,
                opt 
                    => opt.MapFrom(src => src.RateAnswer));
        CreateMap<ScaleAnswerRequestModel, ScaleQuestionAnswer>()
            .ForMember(dest => dest.Scale,
                opt =>
                    opt.MapFrom(src => src.ScaleAnswer));
        CreateMap<RadioAnswerRequestModel, RadioQuestionAnswer>();
        CreateMap<CheckboxAnswerRequestModel, CheckboxQuestionAnswer>()
            .ForMember(dest => dest.Options, 
                opt => 
                    opt.MapFrom(src => src.OptionIds
                        .Select(option => new OptionQuestionAnswer
                    {
                        OptionId = option
                    })));

        CreateMap<CheckboxQuestionAnswerQueue, CheckboxQuestionAnswer>();
        CreateMap<OptionQuestionAnswerQueue, OptionQuestionAnswer>();
    }
}