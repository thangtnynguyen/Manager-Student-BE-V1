using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Config;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class ConfigMapper : Profile
    {
        public ConfigMapper()
        {
            CreateMap<Config, ConfigDto>();
            CreateMap<ConfigDto, Config>();
        }
    }
}
