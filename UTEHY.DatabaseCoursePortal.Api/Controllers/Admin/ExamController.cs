using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Exam;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.QuestionCategory;
using UTEHY.DatabaseCoursePortal.Api.Models.Teacher;
using UTEHY.DatabaseCoursePortal.Api.Services;
using UTEHY.DatabaseCoursePortal.Api.Models.ExamResult;
using UTEHY.DatabaseCoursePortal.Api.Models.Question;
using DocumentFormat.OpenXml.Drawing;
using FluentValidation;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers.Admin
{
    [Route("api/admin/exam")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly ExamService _examService;

        public ExamController(ExamService examService)
        {
            _examService = examService;
        }

        [HttpGet("get")]
        public async Task<ApiResult<PagingResult<Exam>>> Get([FromQuery] GetExamRequest request)
        {
            var result = await _examService.Get(request);

            return new ApiResult<PagingResult<Exam>>()
            {
                Status = true,
                Message = "Lấy thông tin danh sách đề thi thành công!",
                Data = result
            };
        }

        [HttpGet("get-by-id")]
        public async Task<ApiResult<ExamDto>> GetById([FromQuery] int id)
        {
            var result = await _examService.GetById(id);

            return new ApiResult<ExamDto>()
            {
                Status = true,
                Message = "Danh sách Exam đã được lấy thành công!",
                Data = result
            };
        }

        [HttpPost("create")]    
        public async Task<ApiResult<bool>> Create([FromForm] CreateExamRequest request)
        {
            var result = await _examService.Create(request);

            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Tạo đề thi thành công",
                Data = true
            };
        }



        [HttpPost("delete")]
        public async Task<ApiResult<Exam>> Delete([FromBody] EntityIdentityRequest<int> request)
        {
            var result = await _examService.Delete(request.Id);

            return new ApiResult<Exam>()
            {
                Status = true,
                Message = "Đã xóa các bài thi!",
                Data = result
            };
        }

        [HttpPost("delete-multiple")]
        public async Task<ApiResult<List<Exam>>> DeleteMultiple([FromBody] ListEntityIdentityRequest<int?> request)
        {
            var result = await _examService.DeleteMultiple(request.Ids);

            return new ApiResult<List<Exam>>()
            {
                Status = true,
                Message = "Đã xóa các bài thi",
                Data = result
            };
        }

        [HttpGet("get-result")]
        public async Task<ApiResult<PagingResult<ExamResultDto>>> GetExamResults([FromQuery] GetExamResultRequest request)
        {
            var result = await _examService.GetExamResults(request);

            return new ApiResult<PagingResult<ExamResultDto>>()
            {
                Status = true,
                Message = "Lấy thông tin danh sách điểm  thi thành công!",
                Data = result
            };
        }


        [HttpPost("test-autoAdd")]
        public async Task<ApiResult<List<Question>>> AutoCreateExam(GetQuestionAuto request)
        {
            var results = await _examService.AutoGetQuestion(request);

            return new ApiResult<List<Question>>()
            {
                Status = true,
                Message = "kiểm tra thành công",
                Data = results
            };
        }



        [HttpPost("check-answers")]
        public async Task<ApiResult<CheckQuestionResult>> CheckAnswers(List<CheckQuestionRequest> questionsToCheck)
        {
            var results = await _examService.CheckAnswers(questionsToCheck);

            return new ApiResult<CheckQuestionResult>()
            {
                Status = true,
                Message = "kiểm tra thành công",
                Data = results
            };
        }

        [HttpPost("add-exam-result")]
        public async Task<ApiResult<bool>> AddExamResult([FromBody] CreateExamResultRequest request)
        {
            var result = await _examService.AddExamResult(request);

            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Tạo đề thi thành công",
                Data = true
            };
        }

        [HttpGet("get-exam-result-by-student-exam-id")]
        public async Task<ApiResult<ExamResultDto>> GetExamResult([FromQuery] GetOneExamByStudent request)
        {
            var result = await _examService.GetExamResult(request.StudentId, request.ExamId);

            return new ApiResult<ExamResultDto>()
            {
                Status = true,
                Message = "Thành công",
                Data = result
            };
        }


    }
}

