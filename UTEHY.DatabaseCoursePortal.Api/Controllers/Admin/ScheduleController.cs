using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Schedule;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ScheduleService _scheduleService;

        public ScheduleController(ScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpPost("create")]
        public async Task<ApiResult<Schedule>> Create([FromBody] CreateScheduleRequest request)
        {
            var result = await _scheduleService.Create(request);
            return new ApiResult<Schedule>()
            {
                Status = true,
                Message = "Tạo mới lịch học thành công!",
                Data = result
            };
        }

        [HttpPost("create-list-schedule")]
        public async Task<ApiResult<bool>> CreateListSchedule([FromBody] List<CreateScheduleRequest> request)
        {

            var result = await _scheduleService.CreateListSchedule(request);

            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Tạo mới lịch học thành công!",
                Data = result
            };
        }

        [HttpGet("get")]
        public async Task<ApiResult<PagingResult<Schedule>>> Get([FromQuery] GetScheduleRequest request)
        {
            var result = await _scheduleService.Get(request);
            return new ApiResult<PagingResult<Schedule>>()
            {
                Status = true,
                Message = "Lấy thông tin danh sách lịch học thành công",
                Data = result
            };
        }

        [HttpGet("export-excel-attendence-sheet")]
        public async Task<IActionResult> ExportAttendenceSheet([FromQuery] GetScheduleRequest request)
        {
            var excelBytes = await _scheduleService.ExportAttendenceSheet(request);

            return File(excelBytes, Constants.File.ExcelMime, ExportFile.AttendenceSheetExcel);
        }
    }
}
