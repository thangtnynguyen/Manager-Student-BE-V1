using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Attributes;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Student;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("get")]
        public async Task<ApiResult<PagingResult<StudentDto>>> Get([FromQuery] GetStudentRequest request)
        {
            var result = await _studentService.Get(request);

            return new ApiResult<PagingResult<StudentDto>>()
            {
                Status = true,
                Message = "Lấy thông tin danh sách sinh viên thành công!",
                Data = result
            };
        }

        [HttpPost("create")]
        public async Task<ApiResult<Data.Entities.Student>> Create([FromBody] CreateStudentRequest request)
        {
            var result = await _studentService.Create(request);

            return new ApiResult<Data.Entities.Student>()
            {
                Status = true,
                Message = "Tạo mới sinh viên thành công!",
                Data = result
            };
        }

        [HttpGet("get-by-id")]
        public async Task<ApiResult<StudentDto>> GetById([FromQuery]int id)
        {
            var result = await _studentService.GetById(id);

            return new ApiResult<StudentDto>()
            {
                Status = true,
                Message = "Lấy thông tin sinh viên thành công!",
                Data = result
            };
        }

        [HttpPost("edit")]
        public async Task<ApiResult<StudentDto>> Edit([FromBody] EditStudentRequest request)
        {
            var result = await _studentService.Edit(request);

            return new ApiResult<StudentDto>()
            {
                Status = true,
                Message = "Cập nhật sinh viên thành công!",
                Data = result
            };
        }

        [HttpPost("delete")]
        public async Task<ApiResult<StudentDto>> Delete([FromBody] DeleteRequest request)
        {
            var result = await _studentService.Delete(request.Id);

            return new ApiResult<StudentDto>()
            {
                Status = true,
                Message = "Xoá sinh viên thành công!",
                Data = result
            };
        }

        [HttpPost("delete-multiple")]
        public async Task<ApiResult<DeleteMultipleResult<int>>> DeleteMultiple([FromBody] DeleteMultipleRequest request)
        {
            var result = await _studentService.DeleteMultiple(request.Ids);

            var successMessage = result.FailedItems.Count > 0
                ? $"Xoá thành công các sinh viên có ID: {string.Join(", ", result.SuccessfulItems)}"
                : "Xoá nhiều sinh viên thành công!";

            var failureMessage = result.FailedItems.Count > 0
                ? $"Không tìm thấy sinh viên có ID: {string.Join(", ", result.FailedItems)}"
                : string.Empty;

            var message = $"{successMessage}\n{failureMessage}".Trim();

            return new ApiResult<DeleteMultipleResult<int>>()
            {
                Status = true,
                Message = message,
                Data = result
            };
        }
    }
}
