namespace UTEHY.DatabaseCoursePortal.Api.Models.GroupModule
{
    public class RemoveMultipleStudentsRequest
    {
        public int? GroupModuleId { get; set; }

        public List<int?> StudentIds { get; set; }
    }
}
