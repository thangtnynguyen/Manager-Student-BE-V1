using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Banner;
using UTEHY.DatabaseCoursePortal.Api.Models.Comment;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class BannerMapper : Profile
    {
        public BannerMapper()
        {
            CreateMap<Banner, CreateBannerRequest>();
            CreateMap<CreateBannerRequest, Banner>();
            CreateMap<RequestCommentBlogViewModel, Comment>();
            CreateMap<Comment, RequestCommentBlogViewModel>();
            CreateMap<EditBannerRequest, Banner>();
            CreateMap<Banner, EditBannerRequest>();
            CreateMap<Banner, BannerDto>();
            CreateMap<BannerDto, Banner>();
        }
    }
}
