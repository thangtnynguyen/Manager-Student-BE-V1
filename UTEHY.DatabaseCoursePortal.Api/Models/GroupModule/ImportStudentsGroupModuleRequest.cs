namespace UTEHY.DatabaseCoursePortal.Api.Models.GroupModule
{
    public class ImportStudentsGroupModuleRequest
    {
        public IFormFile File { get; set; }

        public int GroupModuleId { get; set; }

        public string PasswordStudent { get; set;}
    }
}
