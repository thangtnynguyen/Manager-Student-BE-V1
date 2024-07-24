using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Configs;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Helpers;
using UTEHY.DatabaseCoursePortal.Api.Models.Post;
using UTEHY.DatabaseCoursePortal.Api.Models.Student;
using UTEHY.DatabaseCoursePortal.Api.Models.Teacher;
using UTEHY.DatabaseCoursePortal.Api.Models.User;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<CreateUserRequest, CreateTeacherRequest>();
            CreateMap<CreateUserRequest, CreateStudentRequest>();

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
