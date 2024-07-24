using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Configs;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Helpers;
using UTEHY.DatabaseCoursePortal.Api.Models.Post;
using UTEHY.DatabaseCoursePortal.Api.Models.Teacher;
using UTEHY.DatabaseCoursePortal.Api.Models.User;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class TeacherMapper : Profile
    {
        public TeacherMapper()
        {
            CreateMap<Teacher, TeacherDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.Name))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.AvatarUrl, opt => opt.MapFrom(src => SystemConfig.BaseUrl + src.User.AvatarUrl))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => PhoneHelper.FormatPhoneNumber(src.User.PhoneNumber)))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.User.Status));

            CreateMap<CreateTeacherRequest, CreateUserRequest>();
        }
    }
}
