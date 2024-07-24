using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Permisson;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class PermissionMapper:Profile
    {
        public PermissionMapper()
        {

           // CreateMap<Permission, PermissionDto>()
           //.ForMember(dest => dest.Children, opt => opt.Ignore());
            CreateMap<Permission, PermissionDto>();
            CreateMap<PermissionDto, Permission>();


        }
    }
}
