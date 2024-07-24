namespace UTEHY.DatabaseCoursePortal.Api.Models.Permisson
{
    public class GetPermissonRequest
    {
        public Guid? RoleId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
