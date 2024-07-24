using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Question;
using UTEHY.DatabaseCoursePortal.Api.Models.Tag;
using UTEHY.DatabaseCoursePortal.Api.Models.Teacher;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers.Admin
{
    [Route("api/admin/tag")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly TagService _tagService;

        public TagController(TagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet("get")]
        public async Task<ApiResult<List<Tag>>> Get([FromQuery] GetTagRequestcs request)
        {
            var result = await _tagService.Get(request);

            return new ApiResult<List<Tag>>()
            {
                Status = true,
                Message = "Lấy danh sách thẻ tag thành công!",
                Data = result
            };
        }

        [HttpPost("create")]
        public async Task<ApiResult<Tag>> Create([FromBody] CreateTagRequest request)
        {
            var result = await _tagService.Create(request);

            return new ApiResult<Tag>()
            {
                Status = true,
                Message = "Tạo mới tag thành công!",
                Data = result
            };
        }
        [HttpPost("edit")]
        public async Task<ApiResult<Tag>> Edit([FromBody] EditTagRequest request)
        {
            var result = await _tagService.Edit(request);

            return new ApiResult<Tag>()
            {
                Status = true,
                Message = "Cập nhật tag thành công!",
                Data = result
            };
        }

        [HttpPost("delete-multiple")]
        public async Task<ApiResult<DeleteMultipleResult<int>>> DeleteMultiple([FromBody] DeleteMultipleRequest request)
        {
            var result = await _tagService.DeleteMultiple(request.Ids);

            var successMessage = result.FailedItems.Count > 0
                ? $"Xoá thành công các  tag có ID: {string.Join(", ", result.SuccessfulItems)}"
                : "Xoá nhiều  tag thành công!";

            var failureMessage = result.FailedItems.Count > 0
                ? $"Không tìm thấy  tag có ID: {string.Join(", ", result.FailedItems)}"
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
