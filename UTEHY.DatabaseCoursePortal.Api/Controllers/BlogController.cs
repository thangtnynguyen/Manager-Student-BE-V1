using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Blog;
using UTEHY.DatabaseCoursePortal.Api.Models.Comment;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet("get")]
        public async Task<ApiResult<PagingResult<Blog>>> Get([FromQuery] PagingRequest request)
        {
            var result = await _blogService.Get(request);

            return new ApiResult<PagingResult<Blog>>()
            {
                Status = true,
                Message = "Danh sách bài viết đã được lấy thành công!",
                Data = result
            };
        }
        [HttpPost("post")]
        public async Task<ApiResult<Blog>> Create([FromForm] CreateBlogRequest request)
        {
            var result = await _blogService.Create(request);

            return new ApiResult<Blog>()
            {
                Status = true,
                Message = "Bài viết đã được thêm thành công!",
                Data = result
            };
        }
        [HttpPut("put")]
        public async Task<ApiResult<Blog>> Edit([FromForm] EditBlogRequest request)
        {
            var result = await _blogService.Edit(request);

            return new ApiResult<Blog>()
            {
                Status = true,
                Message = "Bài viết đã được sửa thành công!",
                Data = result
            };
        }
        [HttpDelete("delete")]
        public async Task<ApiResult<Blog>> Delete([FromBody] int id)
        {
            var result = await _blogService.Delete(id);

            return new ApiResult<Blog>()
            {
                Status = true,
                Message = "Xoá bài viết thành công!",
                Data = result
            };
        }

        [HttpGet("get-blog-by-topic-id")]
        public async Task<ApiResult<PagingResult<Blog>>> GetListBlogByIdTopic([FromQuery] PagingRequest request, int topicId)
        {
            var result = await _blogService.GetListBlogByIdTopic(topicId, request);

            return new ApiResult<PagingResult<Blog>>()
            {
                Status = true,
                Message = "Xoá bài viết thành công!",
                Data = result
            };

        }

        [HttpGet("get-comment-blog")]
        public async Task<ApiResult<List<Comment>>> GetCommentBlog(int blogId)
        {
            var listComment = await _blogService.GetCommentBlog(blogId);
            if (listComment != null)
            {
                return new ApiResult<List<Comment>>()
                {
                    Status = true,
                    Message = "Lấy bình luận thành công!",
                    Data = listComment
                };
            }
            return new ApiResult<List<Comment>>()
            {
                Status = true,
                Message = "Bài viết chưa có bình luận!",
                Data = null
            };
        }
        [HttpGet("get-comment-by-commentparent")]
        public async Task<ApiResult<List<Comment>>> GetCommentByCommentParentId(int commnetParentId)
        {
            var listComment = await _blogService.GetCommentByCommentParentId(commnetParentId);
            if (listComment != null)
            {
                return new ApiResult<List<Comment>>()
                {
                    Status = true,
                    Message = "Lấy bình luận con thành công!",
                    Data = listComment
                };
            }
            return new ApiResult<List<Comment>>()
            {
                Status = true,
                Message = "Bình luận chưa có trả lời!",
                Data = null
            };
        }
        [HttpPost("create-comment-blog")]
        public async Task<ApiResult<Comment>> CreateCommentBlog(RequestCommentBlogViewModel requestComment)
        {
            // Comment result;

            var result = await _blogService.CreateCommentBlog(requestComment);

            if (result != null)
            {
                return new ApiResult<Comment>()
                {
                    Status = true,
                    Message = "Bình luận thành công!",
                    Data = result
                };
            }
            return new ApiResult<Comment>()
            {
                Status = true,
                Message = "Có lỗi xảy ra khi bình luận! Vui lòng thử lại",
                Data = null
            };
        }
    }
}
