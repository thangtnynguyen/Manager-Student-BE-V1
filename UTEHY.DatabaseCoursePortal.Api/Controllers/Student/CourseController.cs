using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Attributes;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Course;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers.Student
{
    [Route("api/student/[controller]")]
    [ApiController]
    [HasPermission(Constants.Permission.Student)]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        //[HttpGet("get-course-learning-user")]
        //public async Task<ApiResult<CourseLearningUser>> GetCourseLearningUser([FromQuery] GetCourseLearningUserRequest request)
        //{
        //    var result = await _courseService.GetCourseLearningUser(request, HttpContext);

        //    return new ApiResult<CourseLearningUser>()
        //    {
        //        Status = true,
        //        Message = "Lấy thông tin khoá học thành công!",
        //        Data = result
        //    };
        //}

        //[HttpPost("register-course-student")]
        //public async Task<ApiResult<bool>> RegisterCourseStudent([FromBody] RegisterCourseStudentRequest request)
        //{
        //    var result = await _courseService.RegisterCourseStudent(request, HttpContext);

        //    return new ApiResult<bool>()
        //    {
        //        Status = true,
        //        Message = "Đăng ký thông tin khoá học thành công!",
        //        Data = true
        //    };
        //}
    }
}
