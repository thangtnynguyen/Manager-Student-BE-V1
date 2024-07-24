using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using UTEHY.DatabaseCoursePortal.Api.Attributes;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Post;
using UTEHY.DatabaseCoursePortal.Api.Models.Teacher;
using UTEHY.DatabaseCoursePortal.Api.Services;
namespace UTEHY.DatabaseCoursePortal.Api.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;
        public PostController(PostService postService)
        {
            _postService = postService;
        }
        [HttpGet("get")]
        public async Task<ApiResult<PagingResult<PostHomeDto>>> Get([FromQuery] GetPostRequest request)
        {
            var result = await _postService.Get(request);

            return new ApiResult<PagingResult<PostHomeDto>>()
            {
                Status = true,
                Message = "Lấy thông tin bài viết thành công!",
                Data = result
            };
        }
        [HttpGet("get-by-id")]
        public async Task<ApiResult<PostHomeDto>> GetById([FromQuery] int id)
        {
            var result = await _postService.GetById(id);

            return new ApiResult<PostHomeDto>()
            {
                Status = true,
                Message = "Lấy thông tin bài viết thành công!",
                Data = result
            };
        }

        [HttpPost("delete")]
        public async Task<ApiResult<PostHomeDto>> Delete([FromBody] DeleteRequest request)
        {
            var result = await _postService.Delete(request.Id);

            return new ApiResult<PostHomeDto>()
            {
                Status = true,
                Message = "Xoá bài viết thành công!",
                Data = result
            };
        }
        [HttpPost("edit")]
        public async Task<ApiResult<PostHomeDto>> Edit([FromBody] EditPostRequest request)
        {
            var result = await _postService.Edit(request);

            return new ApiResult<PostHomeDto>()
            {
                Status = true,
                Message = "Cập nhật bài viết thành công!",
                Data = result
            };
        }
        [HttpPost("approve")]
        public async Task<ApiResult<PostHomeDto>> Approve([FromBody] ApprovePostRequest request)
        {
            var result = await _postService.Approve(request);

            return new ApiResult<PostHomeDto>()
            {
                Status = true,
                Message = "Phê duyệt  bài viết thành công!",
                Data = result
            };
        }
        [HttpPost("approve-multiple")]
        public async Task<ApiResult<ApproveMultipleCommentResult<int>>> ApproveMultiple([FromBody] List<ApprovePostRequest> requests)
        {
            var result = await _postService.ApproveMultiple(requests);

            return new ApiResult<ApproveMultipleCommentResult<int>>()
            {
                Status = true,
                Message = "Phê duyệt  bài viết thành công!",
                Data = result
            };
        }
        [HttpPost("create")]
        public async Task<ApiResult<PostHomeDto>> Create([FromForm] CreatePostRequest request)
        {
            var result = await _postService.Create(request);

            return new ApiResult<PostHomeDto>()
            {
                Status = true,
                Message = "Tạo mới bài viết thành công!",
                Data = result
            };
        }
        [HttpGet("get-approve")]
        public async Task<ApiResult<PagingResult<PostHomeDto>>> GetApprove([FromQuery] GetPostRequest request)
        {
            var result = await _postService.GetApprove(request);

            return new ApiResult<PagingResult<PostHomeDto>>()
            {
                Status = true,
                Message = "Lấy thông tin bài viết thành công!",
                Data = result
            };
        }

        [HttpPost("import-posts")]
        public async Task<bool> Import([FromForm] IFormFile formFile)
        {
            using var stream = new MemoryStream();
            await formFile.CopyToAsync(stream);
            using var package = new ExcelPackage(stream);
            ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
            var rowcount = worksheet.Dimension.Rows;

            for (int row = 2; row <= rowcount; row++)
            {
                CreatePostRequest teacher = new CreatePostRequest
                {
                    Title = worksheet.Cells[row, 1].Value.ToString().Trim(),
                    Description = worksheet.Cells[row, 2].Value.ToString().Trim(),
                    Content = worksheet.Cells[row, 3].Value.ToString().Trim(),
                    Priority = 1,
                    IsPublished = true,
                };

                await _postService.Create(teacher);
            }

            return true;
        }

    }
}
