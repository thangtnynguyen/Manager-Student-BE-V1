namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class UserGroup
    {
        public int Id { get; set; }

        public Guid? UserId { get; set; }

        public int? GroupId { get; set; }
    }
}
