using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.LessonContent;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class LessonContentController : ControllerBase
    {
        private readonly LessonContentService _lessonContentService;

        public LessonContentController(LessonContentService lessonContentService)
        {
            _lessonContentService = lessonContentService;
        }

        #region Manage lesson content
        [HttpGet("get-lesson-content-by-lesson-id")]
        public async Task<ApiResult<List<LessonContent>>> GetLessonContentByLessonId([FromQuery] int id)
        {
            try
            {
                var result = await _lessonContentService.GetLessonContentByLessonId(id);

                return new ApiResult<List<LessonContent>>()
                {
                    Status = true,
                    Message = "Lấy danh sách nội dung bài học thành công!",
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
        public async Task<ApiResult<LessonContent>> Create([FromForm] LessonContentModel request)
        {
            var result = await _lessonContentService.Create(request);

            return new ApiResult<LessonContent>()
            {
                Status = true,
                Message = "Thêm nội dung bài học thành công!",
                Data = result
            };
        }

        [HttpPost("edit")]
        public async Task<ApiResult<LessonContent>> Edit([FromForm] LessonContentModel request)
        {
            var result = await _lessonContentService.Edit(request);

            return new ApiResult<LessonContent>()
            {
                Status = true,
                Message = "Sửa nội dung bài học thành công!",
                Data = result
            };
        }

        [HttpPost("delete")]
        public async Task<ApiResult<LessonContent>> Delete(EntityIdentityRequest<int> request)
        {
            var result = await _lessonContentService.Delete(request.Id);

            return new ApiResult<LessonContent>()
            {
                Status = true,
                Message = "Xoá tài liẹu thành công!",
                Data = result
            };
        }
        #endregion

        [HttpGet("get")]
        public async Task<ApiResult<PagingResult<LessonContent>>> Get([FromQuery] GetLessonContentRequest request)
        {
            var result = await _lessonContentService.Get(request);

            return new ApiResult<PagingResult<LessonContent>>()
            {
                Status = true,
                Message = "Lấy thông tin danh sách nội dung bài giảng thành công!",
                Data = result
            };
        }
    }
}
