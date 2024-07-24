using AutoMapper;
using Google.Apis.YouTube.v3.Data;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Banner;
using UTEHY.DatabaseCoursePortal.Api.Models.Comment;
using UTEHY.DatabaseCoursePortal.Api.Models.GoogleClould;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class GoogleClouldMapper : Profile
    {
        public GoogleClouldMapper()
        {
            CreateMap<Video, VideoYoutube>()
           .ForMember(dest => dest.UrlVideo, opt => opt.MapFrom(src => GoogleClouldConstant.BaseUrlVideo + src.Id)); ;
        }
    }
}
