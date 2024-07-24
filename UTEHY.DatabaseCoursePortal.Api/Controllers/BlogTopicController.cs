using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.BlogTopic;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogTopicController : ControllerBase
    {
        private readonly BlogTopicService _blogTopicService;
        public BlogTopicController(BlogTopicService blogTopicService)
        {
            _blogTopicService = blogTopicService;
        }

        [HttpGet("get")]
        public async Task<ApiResult<List<BlogTopic>>> Get()
        {
            var result = await _blogTopicService.Get();

            return new ApiResult<List<BlogTopic>>()
            {
                Status = true,
                Message = "Danh sách chủ đề đã được lấy thành công!",
                Data = result
            };
        }
        [HttpPost("post")]
        public async Task<ApiResult<BlogTopic>> Create([FromForm] RequestBlogTopicViewModel request)
        {
            var result = await _blogTopicService.Create(request);

            return new ApiResult<BlogTopic>()
            {
                Status = true,
                Message = "Chủ đề đã được thêm thành công!",
                Data = result
            };
        }
        [HttpPut("put")]
        public async Task<ApiResult<BlogTopic>> Edit([FromForm] RequestBlogTopicViewModel request, int id)
        {
            var result = await _blogTopicService.Edit(request, id);

            return new ApiResult<BlogTopic>()
            {
                Status = true,
                Message = "Chủ đề đã được sửa thành công!",
                Data = result
            };
        }
        [HttpDelete("delete")]
        public async Task<ApiResult<BlogTopic>> Delete([FromBody] int id)
        {
            var result = await _blogTopicService.Delete(id);

            return new ApiResult<BlogTopic>()
            {
                Status = true,
                Message = "Xoá chủ đề thành công!",
                Data = result
            };
        }
    }
}
