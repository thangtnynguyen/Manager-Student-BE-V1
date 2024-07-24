using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Course;
using UTEHY.DatabaseCoursePortal.Api.Models.Track;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers
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

        //[HttpPost("create-course")]
        //public async Task<ActionResult<ApiResult<Course>>> CreateCourse([FromQuery] CreateCourseRequest request)
        //{
        //    var course = await _courseService.CreateCourse(request);
        //    return new ApiResult<Course>
        //    {
        //        Status = true,
        //        Message = "Khóa học được tạo thành công!",
        //        Data = course
        //    };
        //}

        //[HttpPost("create-track-by-courseid")]
        //public async Task<ActionResult<ApiResult<Track>>> CreateTrackByCourseId([FromQuery] CreateTrackRequest request)
        //{
        //    var track = await _courseService.CreateTrackByCourseId(request);
        //    return new ApiResult<Track>
        //    {
        //        Status = true,
        //        Message = "Track được tạo thành công!",
        //        Data = track
        //    };
        //}

        //[HttpPost("create-steptrack-by-trackid")]
        //public async Task<ActionResult<ApiResult<StepTrack>>> CreateStepTrackByTrackId(CreateStepTrackRequest request)
        //{
        //    var stepTrack = await _courseService.CreateStepTrackByTrackId(request);
        //    return new ApiResult<StepTrack>
        //    {
        //        Status = true,
        //        Message = "Step track được tạo thành công!",
        //        Data = stepTrack
        //    };
        //}

        //[HttpPost("create-assignment-course")]
        //public async Task<ActionResult<ApiResult<AssignmentCourse>>> CreateAssignmentCourse(CreateAssignmentCourseRequest request)
        //{
        //    var assigmentCourse = await _courseService.CreateAssignment(request);

        //    return new ApiResult<AssignmentCourse>
        //    {
        //        Status = true,
        //        Message = "Bài tập được tạo thành công!",
        //        Data = assigmentCourse
        //    };
        //}


        //[HttpGet("get-course-by-id")]
        //public async Task<ActionResult<ApiResult<Course>>> GetCourseById(int id)
        //{
        //    var course = await _courseService.GetCourseById(id);
        //    return new ApiResult<Course>
        //    {
        //        Status = true,
        //        Message = "Khóa học được lấy thành công!",
        //        Data = course
        //    };
        //}

        //[HttpGet("get-courses")]
        //public async Task<ActionResult<ApiResult<PagingResult<Course>>>> GetCourses([FromQuery] PagingRequest request)
        //{
        //    var courses = await _courseService.GetCourses(request);
        //    return new ApiResult<PagingResult<Course>>
        //    {
        //        Status = true,
        //        Message = "Danh sách khóa học được lấy thành công!",
        //        Data = courses
        //    };
        //}
        //[HttpGet("get-tracks-by-courseid")]
        //public async Task<ActionResult<ApiResult<IEnumerable<Track>>>> GetTrackByCourseId(int courseId)
        //{
        //    var tracks = await _courseService.GetTracksByCourseId(courseId);

        //    return new ApiResult<IEnumerable<Track>>
        //    {
        //        Status = true,
        //        Message = "Danh sách track được lấy thành công!",
        //        Data = tracks
        //    };
        //}
        //[HttpGet("get-steptracks-by-trackid")]
        //public async Task<ActionResult<ApiResult<IEnumerable<CreateStepTrackRequest>>>> GetStepTrackByTrackId(int stepTrackId)
        //{
        //    var tracks = await _courseService.GetStepTracksByTrackId(stepTrackId);

        //    return new ApiResult<IEnumerable<CreateStepTrackRequest>>
        //    {
        //        Status = true,
        //        Message = "Danh sách step track được lấy thành công!",
        //        Data = tracks
        //    };
        //}

    }
}
