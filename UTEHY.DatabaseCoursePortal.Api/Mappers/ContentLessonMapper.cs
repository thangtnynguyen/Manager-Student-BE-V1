using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.LessonContent;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class ContentLessonMapper : Profile
    {
        public ContentLessonMapper()
        {
            CreateMap<LessonContent, CreateLessonContentRequest>();
            CreateMap<CreateLessonContentRequest, LessonContent>();

            CreateMap<LessonContent, LessonContent>();

            CreateMap<LessonContentModel, LessonContent>();
        }
    }
}
