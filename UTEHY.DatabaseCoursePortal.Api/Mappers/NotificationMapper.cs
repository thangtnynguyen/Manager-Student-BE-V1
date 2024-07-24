using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Banner;
using UTEHY.DatabaseCoursePortal.Api.Models.Comment;
using UTEHY.DatabaseCoursePortal.Api.Models.Notification;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class NotificationMapper : Profile
    {
        public NotificationMapper()
        {
            CreateMap<Notification, GetNotificationRequest>();
            CreateMap<GetNotificationRequest, Notification>();
            CreateMap<Notification, NotificationDto>()
              .ForMember(dest => dest.GroupModules, opt => opt.MapFrom(src => src.NotificationGroupModules.Select(rp => rp.GroupModule)));
            CreateMap<NotificationDto, Notification>();

            CreateMap<EditNotificationRequest, Notification>();
            //CreateMap<Notification, EditNotificationRequest>();
            CreateMap<CreateNotificationRequest, Notification>();
            //CreateMap<Notification, CreateNotificationRequest>();

        }
    }
}
