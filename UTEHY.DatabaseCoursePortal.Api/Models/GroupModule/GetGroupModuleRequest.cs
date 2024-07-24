using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Models.GroupModule
{
    public class GetGroupModuleRequest : PagingRequest
    {
        public string? Name { get; set; }

        public bool? Status { get; set; }  

        public int? Year { get; set; }

        public int? Semester { get; set; }

        public int? TeacherId { get; set; }
    }
}
