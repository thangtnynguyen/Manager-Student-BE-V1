using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Services;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Course;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers.Admin
{
    [Route("api/admin/course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet("get")]
        public async Task<ApiResult<Course>> GetCourse()
        {
            try
            {
                var result = await _courseService.GetCourse();

                return new ApiResult<Course>()
                {
                    Status = true,
                    Message = "Lấy thông tin khoá học thành công!",
                    Data = result
                };
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("edit")]
        public async Task<ApiResult<Course>> EditCourse(EditCourseRequest request)
        {
            try
            {
                var result = await _courseService.EditCourse(request);

                return new ApiResult<Course>()
                {
                    Status = true,
                    Message = "Cập nhật thông tin khoá học thành công!",
                    Data = result
                };
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
