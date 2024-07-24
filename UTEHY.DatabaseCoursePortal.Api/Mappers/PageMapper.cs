using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Config;
using UTEHY.DatabaseCoursePortal.Api.Models.Page;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class PageMapper : Profile
    {
        public PageMapper()
        {
            CreateMap<Page, PageHomeDto>();
            CreateMap<PageHomeDto, Page>();
        }
    }
}
