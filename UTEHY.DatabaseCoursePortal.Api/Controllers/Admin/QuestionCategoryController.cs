using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.QuestionCategory;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers.Admin
{
    [Route("api/admin/question-category")]
    [ApiController]
    public class QuestionCategoryController : ControllerBase
    {
        private readonly QuestionCategoryService _questionCategoryService;

        public QuestionCategoryController(QuestionCategoryService questionCategoryService)
        {
            _questionCategoryService = questionCategoryService;
        }

        [HttpGet("get-tree")]
        public async Task<ApiResult<List<QuestionCategoryDto>>> GetTree([FromQuery] GetQuestionCategoryTreeRequest request)
        {
            var result = await _questionCategoryService.GetTree();

            return new ApiResult<List<QuestionCategoryDto>>()
            {
                Status = true,
                Message = "Lấy thông tin danh sách loại câu hỏi thành công!",
                Data = result
            };
        }

        [HttpPost("create")]
        public async Task<ApiResult<QuestionCategoryDto>> Create([FromBody] CreateQuestionCategoryRequest request)
        {
            var result = await _questionCategoryService.Create(request);

            return new ApiResult<QuestionCategoryDto>()
            {
                Status = true,
                Message = "Tạo mới loại câu hỏi thành công!",
                Data = result
            };
        }

        [HttpPost("edit")]
        public async Task<ApiResult<QuestionCategoryDto>> Edit([FromBody] EditQuestionCategoryRequest request)
        {
            var result = await _questionCategoryService.Edit(request);

            return new ApiResult<QuestionCategoryDto>()
            {
                Status = true,
                Message = "Cập nhật loại câu hỏi thành công!",
                Data = result
            };
        }

        [HttpPost("delete")]
        public async Task<ApiResult<QuestionCategoryDto>> Delete([FromBody] DeleteRequest request)
        {
            var result = await _questionCategoryService.Delete(request);

            return new ApiResult<QuestionCategoryDto>()
            {
                Status = true,
                Message = "Xoá loại câu hỏi thành công!",
                Data = result
            };
        }
    }
}
