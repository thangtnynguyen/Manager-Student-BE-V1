using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Twilio.Http;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.GroupModule;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers.Admin
{
    [Route("api/admin/group-module")]
    [ApiController]
    public class GroupModuleController : ControllerBase
    {
        private readonly GroupModuleService _groupModuleService;

        public GroupModuleController(GroupModuleService groupModuleService)
        {
            _groupModuleService = groupModuleService;
        }

        [HttpGet("get")]
        public async Task<ApiResult<PagingResult<Data.Entities.GroupModule>>> Get([FromQuery] GetGroupModuleRequest request)
        {
            var result = await _groupModuleService.Get(request);

            return new ApiResult<PagingResult<Data.Entities.GroupModule>>()
            {
                Status = true,
                Message = "Lấy thông tin danh sách nhóm học phần thành công!",
                Data = result
            };
        }

        [HttpGet("get-students")]
        public async Task<ApiResult<PagingResult<Data.Entities.Student>>> GetStudentGroupModule([FromQuery] GetStudentsGroupModuleRequest request)
        {
            var result = await _groupModuleService.GetStudentsGroupModule(request);

            return new ApiResult<PagingResult<Data.Entities.Student>>()
            {
                Status = true,
                Message = "Lấy thông tin danh sách sinh viên nhóm học phần thành công!",
                Data = result
            };
        }

        [HttpPost("create")]
        public async Task<ApiResult<Data.Entities.GroupModule>> Create([FromBody] CreateGroupModuleRequest request)
        {
            var result = await _groupModuleService.Create(request);

            return new ApiResult<Data.Entities.GroupModule>()
            {
                Status = true,
                Message = "Tạo mới nhóm học phần thành công!",
                Data = result
            };
        }

        [HttpPost("edit")]
        public async Task<ApiResult<Data.Entities.GroupModule>> Edit([FromBody] EditGroupModuleRequest request)
        {
            var result = await _groupModuleService.Edit(request);

            return new ApiResult<Data.Entities.GroupModule>()
            {
                Status = true,
                Message = "Cập nhật nhóm học phần thành công!",
                Data = result
            };
        }

        [HttpPost("delete")]
        public async Task<ApiResult<Data.Entities.GroupModule>> Delete([FromBody] DeleteRequest request)
        {
            var result = await _groupModuleService.Delete(request.Id);

            return new ApiResult<Data.Entities.GroupModule>()
            {
                Status = true,
                Message = "Xoá nhóm học phần thành công!",
                Data = result
            };
        }

        [HttpPost("hide")]
        public async Task<ApiResult<Data.Entities.GroupModule>> Hide([FromBody] DeleteRequest request)
        {
            var result = await _groupModuleService.Hide(request.Id);

            return new ApiResult<Data.Entities.GroupModule>()
            {
                Status = true,
                Message = "Ẩn nhóm học phần thành công!",
                Data = result
            };
        }

        [HttpGet("get-by-id")]
        public async Task<ApiResult<Data.Entities.GroupModule>> GetById([FromQuery] DeleteRequest request)
        {
            var result = await _groupModuleService.GetById(request.Id);

            return new ApiResult<Data.Entities.GroupModule>()
            {
                Status = true,
                Message = "Lấy thông tin nhóm học phần thành công!",
                Data = result
            };
        }

        [HttpGet("get-by-exam-id")]
        public async Task<ApiResult<List<Data.Entities.GroupModule>>> GetByExamId([FromQuery] DeleteRequest request)
        {
            var result = await _groupModuleService.GetByExamId(request.Id);

            return new ApiResult<List<Data.Entities.GroupModule>>()
            {
                Status = true,
                Message = "Lấy thông tin nhóm học phần thành công!",
                Data = result
            };
        }

        [HttpGet("export-excel-students")]
        public async Task<IActionResult> Export([FromQuery] DeleteRequest request)
        {
            var getStudentRequest = new GetStudentsGroupModuleRequest()
            {
                GroupModuleId = request.Id,
            };

            var studentPaging = await _groupModuleService.GetStudentsGroupModule(getStudentRequest);

            var students = studentPaging.Items;

            var excelBytes = _groupModuleService.ExportStudents(students);

            return File(excelBytes, Constants.File.ExcelMime, ExportFile.ListStudentExcel);
        }

        [HttpGet("export-excel-score-students")]
        public async Task<IActionResult> ExportExcelScoreStudent([FromQuery] DeleteRequest request)
        {
            var excelBytes = await _groupModuleService.ExportScoreStudents(request.Id);

            return File(excelBytes, Constants.File.ExcelMime, ExportFile.ListScoreStudentExcel);
        }

        [HttpPost("add-student")]
        public async Task<ApiResult<StudentGroupModule>> AddStudentGroupModule([FromBody] AddStudentGroupModuleRequest request)
        {
            var result = await _groupModuleService.AddStudentGroupModule(request);

            return new ApiResult<StudentGroupModule>()
            {
                Status = true,
                Message = "Thêm sinh viên vào nhóm học phần thành công!",
                Data = result
            };
        }

        [HttpPost("generate-invitation-code")]
        public async Task<ApiResult<Data.Entities.GroupModule>> GenerateInvitationCode([FromBody] GenerateInvitationCodeRequest request)
        {
            var result = await _groupModuleService.GenerateInvitationCode(request);

            return new ApiResult<Data.Entities.GroupModule>()
            {
                Status = true,
                Message = "Lấy thông tin mã mời thành công!",
                Data = result
            };
        }

        [HttpPost("import-students-excel")]
        public async Task<ApiResult<bool>> ImportStudentsExcel([FromForm] ImportStudentsGroupModuleRequest request)
        {
            await _groupModuleService.ImportStudentsExcel(request);

            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Import file thành công!",
                Data = true
            };
        }

        [HttpPost("remove-student")]
        public async Task<ApiResult<bool>> RemoveStudentGroupModule([FromBody] RemoveStudentRequest request)
        {
            await _groupModuleService.RemoveStudentGroupModule(request);

            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Xoá sinh viên khỏi nhóm thành công!",
                Data = true
            };
        }

        [HttpPost("remove-students")]
        public async Task<ApiResult<bool>> RemoveMultipleStudentsFromGroupModule([FromBody] RemoveMultipleStudentsRequest request)
        {
            await _groupModuleService.RemoveMultipleStudentsFromGroupModule(request);

            return new ApiResult<bool>()
            {
                Status = true,
                Message = "Xoá sinh viên khỏi nhóm thành công!",
                Data = true
            };
        }

        [HttpGet("get-exams")]
        public async Task<ApiResult<List<Exam>>> GetExamsByGroupModule([FromQuery] EntityIdentityRequest<int> request)
        {
            var result = await _groupModuleService.GetExamsByGroupModule(request.Id);

            return new ApiResult<List<Exam>>()
            {
                Status = true,
                Message = "Lấy danh sách bài kiểm tra thành công!",
                Data = result
            };
        }

        [HttpGet("get-notifications")]
        public async Task<ApiResult<List<Notification>>> GetNotificationsByGroupModule([FromQuery] EntityIdentityRequest<int> request)
        {
            var result = await _groupModuleService.GetNotificationsByGroupModule(request.Id);

            return new ApiResult<List<Notification>>()
            {
                Status = true,
                Message = "Lấy danh sách thông báo thành công!",
                Data = result
            };
        }

        [HttpGet("get-group-module-by-user")]
        public async Task<ApiResult<List<Data.Entities.GroupModule>>> GetGroupModuleByUser([FromQuery] Guid userId)
        {

            var result = await _groupModuleService.GetGroupModuleByUser(userId);

            return new ApiResult<List<Data.Entities.GroupModule>>()
            {
                Status = true,
                Message = "Lấy danh sách nhóm thành công!",
                Data = result
            };

        }
    }
}
