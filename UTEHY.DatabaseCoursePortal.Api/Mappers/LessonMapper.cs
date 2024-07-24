using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Lesson;
using UTEHY.DatabaseCoursePortal.Api.Models.Comment;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class LessonMapper : Profile
    {
        public LessonMapper()
        {
            CreateMap<Lesson, CreateLessonRequest>();
            CreateMap<CreateLessonRequest, Lesson>();
            CreateMap<RequestCommentBlogViewModel, Comment>();
            CreateMap<Comment, RequestCommentBlogViewModel>();
            CreateMap<EditLessonRequest, Lesson>();
            CreateMap<Lesson, EditLessonRequest>();
        }
    }
}
