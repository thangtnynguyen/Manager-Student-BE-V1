using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Models.ExamResult
{
    public class GetExamResultRequest:PagingRequest
    {

        public int ExamId { get; set; }

        public int? GroupModuleId { get; set; }

        public string? StudentName { get; set; }

        public int? Type { get; set;}// type là 1 thì lấy cột Score khác null còn type là 2 thì lấy điểm Score = null 


    }
}
