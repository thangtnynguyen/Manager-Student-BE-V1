using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Notification;
using UTEHY.DatabaseCoursePortal.Api.Models.Role;
using UTEHY.DatabaseCoursePortal.Api.Models.Section;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers.Admin
{
    [Route("api/admin/notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet("get")]
        public async Task<ApiResult<PagingResult<NotificationDto>>> Get([FromQuery] GetNotificationRequest request)
        {
            var result = await _notificationService.Get(request);

            return new ApiResult<PagingResult<NotificationDto>>()
            {
                Status = true,
                Message = "Danh sách thông báo đã được lấy thành công!",
                Data = result
            };
        }

        [HttpPost("create")]
        public async Task<ApiResult<NotificationDto>> Create([FromBody] CreateNotificationRequest request)
        {
            var result = await _notificationService.Create(request);

            return new ApiResult<NotificationDto>()
            {
                Status = true,
                Message = "Thêm thông báo thành công!",
                Data = result
            };
        }

        [HttpPost("edit")]
        public async Task<ApiResult<NotificationDto>> Edit([FromBody] EditNotificationRequest request)
        {
            var result = await _notificationService.Edit(request);

            return new ApiResult<NotificationDto>()
            {
                Status = true,
                Message = "Cập nhập thành công!",
                Data = result
            };
        }

        [HttpPost("delete")]
        public async Task<ApiResult<NotificationDto>> Delete([FromBody] EntityIdentityRequest<int> request)
        {
            var result = await _notificationService.Delete(request.Id);

            return new ApiResult<NotificationDto>()
            {
                Status = true,
                Message = "Đã xóa!",
                Data = result
            };
        }
    }
}
