using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Configs;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Helpers;
using UTEHY.DatabaseCoursePortal.Api.Models.Post;
using UTEHY.DatabaseCoursePortal.Api.Models.Question;
using UTEHY.DatabaseCoursePortal.Api.Models.QuestionAnswer;
using UTEHY.DatabaseCoursePortal.Api.Models.QuestionCategory;
using UTEHY.DatabaseCoursePortal.Api.Models.Teacher;
using UTEHY.DatabaseCoursePortal.Api.Models.User;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class QuestionAnswerMapper : Profile
    {
        public QuestionAnswerMapper()
        {
            CreateMap<QuestionAnswerDto, QuestionAnswer>();

            CreateMap<QuestionAnswer, QuestionAnswerDto>();
        }
    }
}
