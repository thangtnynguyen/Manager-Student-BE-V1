using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.GroupModule;
using UTEHY.DatabaseCoursePortal.Api.Models.Post;
using UTEHY.DatabaseCoursePortal.Api.Models.Question;
using UTEHY.DatabaseCoursePortal.Api.Models.Teacher;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers.Admin
{
    [Route("api/admin/question")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly QuestionService _questionService;

        public QuestionController(QuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet("get")]
        public async Task<ApiResult<PagingResult<QuestionDto>>> Get([FromQuery] GetQuestionRequest request)
        {
            var result = await _questionService.Get(request);

            return new ApiResult<PagingResult<QuestionDto>>()
            {
                Status = true,
                Message = "Lấy thông tin danh sách câu hỏi thành công!",
                Data = result
            };
        }

        [HttpGet("get-by-id")]
        public async Task<ApiResult<QuestionDto>> GetById([FromQuery] int id)
        {
            var result = await _questionService.GetById(id);

            return new ApiResult<QuestionDto>()
            {
                Status = true,
                Message = "Lấy thông tin câu hỏi  thành công!",
                Data = result
            };
        }

        [HttpPost("create")]
        public async Task<ApiResult<bool>> Create([FromBody] CreateQuestionRequest request)
        {
            var result = await _questionService.Create(request);

            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Tạo mới câu hỏi thành công!",
                Data = true
            };
        }

        [HttpPost("edit")]
        public async Task<ApiResult<bool>> Edit([FromBody] EditQuestionRequest request)
        {
            var result = await _questionService.Edit(request);

            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Cập nhật mới câu hỏi thành công!",
                Data = true
            };
        }

        [HttpPost("delete")]
        public async Task<ApiResult<QuestionDto>> Delete([FromBody] DeleteQuestionRequest request)
        {
            var result = await _questionService.Delete(request.Id);

            return new ApiResult<QuestionDto>()
            {
                Status = true,
                Message = "Xoá câu hỏi thành công!",
                Data = result
            };
        }

        [HttpPost("delete-multiple")]
        public async Task<ApiResult<bool>> DeleteMultiple([FromBody] ListEntityIdentityRequest<int?> request)
        {
            var result = await _questionService.DeleteMultiple(request.Ids);

            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Đã xóa các question",
                Data = true
            };
        }


        [HttpPost("check-answers")]
        public async Task<ApiResult<CheckQuestionResult>> CheckAnswers(List<CheckQuestionRequest> questionsToCheck)
        {
            var results = await _questionService.CheckAnswers(questionsToCheck);

            return new ApiResult<CheckQuestionResult>()
            {
                Status = true,
                Message = "kiểm tra thành công",
                Data = results
            };
        }

        [HttpPost("import-questions-excel")]
        public async Task<ApiResult<bool>> ImportQuestionsExcel([FromForm] ImportQuestionsRequest request)
        {
            await _questionService.ImportQuestionsExcel(request);

            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Import file thành công!",
                Data = true
            };
        }

    }
}
