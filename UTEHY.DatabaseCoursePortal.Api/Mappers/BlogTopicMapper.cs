using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.BlogTopic;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers;

public class BlogTopicMapper : Profile
{
    public BlogTopicMapper()
    {
        CreateMap<BlogTopic, RequestBlogTopicViewModel>();
        CreateMap<RequestBlogTopicViewModel, BlogTopic>();
        // chỉ map những trường không null
        CreateMap<RequestBlogTopicViewModel, BlogTopic>().ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember, context) =>
       {
           var request = (RequestBlogTopicViewModel)context.Items["Request"];
           return srcMember != null;
       }));
    }
}