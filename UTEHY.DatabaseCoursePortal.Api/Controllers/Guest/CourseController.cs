using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers.Guest
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        //[HttpGet("get-database-course")]
        //public async Task<ApiResult<Course>> GetDatabaseCourse()
        //{
        //    var result = await _courseService.GetDatabaseCourse();

        //    return new ApiResult<Course>()
        //    {
        //        Status = true,
        //        Message = "Lấy thông tin khoá học thành công!",
        //        Data = result
        //    };
        //}
    }
}
