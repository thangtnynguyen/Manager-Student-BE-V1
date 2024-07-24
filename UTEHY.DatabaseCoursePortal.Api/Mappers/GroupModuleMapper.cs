using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Comment;
using UTEHY.DatabaseCoursePortal.Api.Models.GroupModule;
using UTEHY.DatabaseCoursePortal.Api.Models.Lesson;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class GroupModuleMapper : Profile
    {
        public GroupModuleMapper()
        {
            CreateMap<CreateGroupModuleRequest, GroupModule>();

            CreateMap<EditGroupModuleRequest, GroupModule>();

             CreateMap<GroupModuleDto, GroupModule>();

            CreateMap<GroupModule, GroupModuleDto>();
        }
    }
}
