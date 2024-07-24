using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Permisson
{
    public class GetPermissionRequest:PagingRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? DisplayName { get; set; }


    }
}
