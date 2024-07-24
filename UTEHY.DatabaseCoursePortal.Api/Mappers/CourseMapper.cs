using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Course;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class CourseMapper : Profile
    {
        public CourseMapper()
        {
            CreateMap<Course, CourseHomeDto>()
            .ForMember(dest => dest.NumberRegistrations, opt => opt.MapFrom(src => src.UserCourses != null ? src.UserCourses.Count : 0));

            CreateMap<Course, CourseWithSection>();

            CreateMap<EditDatabaseCourseRequest, Course>();

            CreateMap<Course, CreateCourseRequest>();
            CreateMap<CreateCourseRequest, Course>();

            CreateMap<Course, CourseLearningUser>();
            CreateMap<CourseLearningUser, Course>();

            CreateMap<EditCourseRequest, Course>();
        }
    }
}
