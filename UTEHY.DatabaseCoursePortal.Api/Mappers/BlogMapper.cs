using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Blog;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class BlogMapper : Profile
    {
        public BlogMapper()
        {
            CreateMap<Blog, CreateBlogRequest>();
            CreateMap<CreateBlogRequest, Blog>();
            CreateMap<Blog, EditBlogRequest>();
            CreateMap<EditBlogRequest, Blog>();
        }
    }
}
