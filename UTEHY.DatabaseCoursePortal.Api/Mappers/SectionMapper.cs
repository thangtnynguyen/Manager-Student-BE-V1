using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Configs;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Helpers;
using UTEHY.DatabaseCoursePortal.Api.Models.Section;
using UTEHY.DatabaseCoursePortal.Api.Models.Student;
using UTEHY.DatabaseCoursePortal.Api.Models.User;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class SectionMapper : Profile
    {
        public SectionMapper()
        {
            CreateMap<Section, SectionDto>()
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.UserCreated != null ? src.UserCreated.UserName : null))
            .ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UserUpdated != null ? src.UserUpdated.UserName : null));

            CreateMap<CreateSectionRequest, Section>();

            CreateMap<EditSectionRequest, Section>();
        }
    }
}
