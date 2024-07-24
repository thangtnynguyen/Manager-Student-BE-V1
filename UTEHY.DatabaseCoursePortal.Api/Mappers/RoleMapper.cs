using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Comment;
using UTEHY.DatabaseCoursePortal.Api.Models.Permisson;
using UTEHY.DatabaseCoursePortal.Api.Models.Role;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class RoleMapper:Profile
    {
        public RoleMapper()
        {
            //CreateMap<Role, CreateRoleRequest>();
            //CreateMap<CreateRoleRequest, Role>();
            //CreateMap<EditRoleRequest, Role>();
            //CreateMap<Role, EditRoleRequest>();
            //CreateMap<Role, RoleDto>();
            //CreateMap<RoleDto, Role>();
            //CreateMap<RolePermission, PermissionDto>();


            //CreateMap<Role, CreateRoleRequest>();
            //CreateMap<CreateRoleRequest, Role>();
            //CreateMap<EditRoleRequest, Role>();
            //CreateMap<Role, EditRoleRequest>();
            //CreateMap<Role, RoleDto>()
            //    .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.RolePermissions.Select(rp => rp.Permission)));
            //CreateMap<RoleDto, Role>();

            CreateMap<Role, CreateRoleRequest>();
            CreateMap<CreateRoleRequest, Role>();
            CreateMap<EditRoleRequest, Role>();
            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.RolePermissions.Select(rp => rp.Permission).Where(p => p.ParentPermissionId == null)));
            CreateMap<RoleDto, Role>();

        }
    }
}
