using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Lesson;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers.Admin
{
    [Route("api/admin/lesson")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly LessonService _lessonService;

        public LessonController(LessonService lessonService)
        {
            _lessonService = lessonService;
        }

        #region Manage lesson
        [HttpGet("get-lesson-by-section-id")]
        public async Task<ApiResult<List<Lesson>>> GetLessonBySectionId([FromQuery] int id)
        {
            try
            {
                var result = await _lessonService.GetLessonBySectionId(id);

                return new ApiResult<List<Lesson>>()
                {
                    Status = true,
                    Message = "Lấy danh sách bài học thành công!",
                    Data = result
                };
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            catch (BadHttpRequestException ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<ApiResult<Lesson>> Create(CreateLessonRequest request)
        {
            try
            {
                var result = await _lessonService.Create(request);

                return new ApiResult<Lesson>()
                {
                    Status = true,
                    Message = "Tạo mới bài học thành công!",
                    Data = result
                };
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            catch (BadHttpRequestException ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("edit")]
        public async Task<ApiResult<Lesson>> Edit(EditLessonRequest request)
        {
            try
            {
                var result = await _lessonService.Edit(request);

                return new ApiResult<Lesson>()
                {
                    Status = true,
                    Message = "Cập nhật bài học thành công!",
                    Data = result
                };
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            catch (BadHttpRequestException ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("delete")]
        public async Task<ApiResult<Lesson>> Delete(EntityIdentityRequest<int> request)
        {
            var result = await _lessonService.Delete(request.Id);

            return new ApiResult<Lesson>()
            {
                Status = true,
                Message = "Xoá bài học thành công!",
                Data = result
            };
        }
        #endregion
    }
}
