using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Question;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class QuestionMapper : Profile
    {
        public QuestionMapper()
        {
            CreateMap<Question, QuestionDto>()
            .ForMember(dest => dest.QuestionCategoryName, opt => opt.MapFrom(src => src.QuestionCategory.Name));

            CreateMap<QuestionDto, Question>();

            CreateMap<CreateQuestionRequest, QuestionDto>();

            CreateMap<CreateQuestionRequest, Question>()
            .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.QuestionAnswers.Max(a => a.Score)))
            .ForMember(dest => dest.QuestionAnswers, opt => opt.Ignore());

            CreateMap<EditQuestionRequest, Question>()
           .ForMember(dest => dest.Score, opt => opt.MapFrom(src => src.QuestionAnswers.Max(a => a.Score)))
           .ForMember(dest => dest.QuestionAnswers, opt => opt.Ignore());
        }
    }
}
