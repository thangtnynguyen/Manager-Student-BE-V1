using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Exam
{
    public class GetExamRequest: PagingRequest
    {
        public string? Title { get; set; }

        public Guid? CreateBy { get; set; }

        public List<int>? GroupModuleIds { get; set; }

        //public int? GroupModuleId { get; set; }



    }
}
