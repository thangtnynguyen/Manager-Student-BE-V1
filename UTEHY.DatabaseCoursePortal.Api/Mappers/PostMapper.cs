using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Configs;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Helpers;
using UTEHY.DatabaseCoursePortal.Api.Models.Course;
using UTEHY.DatabaseCoursePortal.Api.Models.Post;
using UTEHY.DatabaseCoursePortal.Api.Models.Teacher;
using UTEHY.DatabaseCoursePortal.Api.Models.User;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class PostMapper : Profile
    {
        public PostMapper()
        {
            CreateMap<Post, PostHomeDto>()
            .ForMember(dest => dest.Image, opt => opt.MapFrom(src => SystemConfig.BaseUrl + src.Image))
            .ForMember(dest => dest.ReadingTime, opt => opt.MapFrom(src => DocumentHelper.CalculateReadingTime(src.Content ?? "", DocumentConfig.WordsPerMinute)))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => new UserProfileDto { Name = src.User.Name }));

            //CreateMap<Cre, CreateUserRequest>();

        }
    }
}
