using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Tag;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class TagMapper : Profile
    {
        public TagMapper()
        {
            CreateMap<CreateTagRequest, Tag>();
        }
    }
}
