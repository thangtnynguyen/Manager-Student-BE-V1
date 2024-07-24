using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Attendence;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers.Admin
{
    [Route("api/admin/attendence")]
    [ApiController]
    public class AttendenceController
    {
        private readonly AttendenceService _attendenceService;

        public AttendenceController(AttendenceService attendenceService)
        {
            _attendenceService = attendenceService;
        }

        [HttpGet("get")]
        public async Task<ApiResult<PagingResult<Attendance>>> Get([FromQuery] GetAttendenceRequest request)
        {
            var result = await _attendenceService.Get(request);

            return new ApiResult<PagingResult<Attendance>>()
            {
                Status = true,
                Message = "Lấy danh sách điểm danh thành công!",
                Data = result
            };
        }

        [HttpPost("create")]
        public async Task<ApiResult<Attendance>> Create([FromBody] CreateAttendenceRequest request)
        {
            var result = await _attendenceService.Create(request);

            return new ApiResult<Attendance>()
            {
                Status = true,
                Message = "Điểm danh thành công!",
                Data = result
            };
        }

        [HttpPost("create-list-attendence")]
        public async Task<ApiResult<bool>> CreateListAttendence([FromBody] List<CreateAttendenceRequest> request)
        {
            var result = await _attendenceService.CreateListAttendence(request);

            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Điểm danh thành công!",
                Data = result
            };
        }
    }
}
