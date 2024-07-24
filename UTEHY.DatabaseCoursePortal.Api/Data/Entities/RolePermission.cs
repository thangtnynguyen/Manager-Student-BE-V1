namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class RolePermission
    {
        public int Id { get; set; }

        public Guid RoleId { get; set; }

        public int PermessionId { get; set; }

        public virtual Role Role { get; set; }

        public virtual Permission Permission { get; set; }  
    }
}
