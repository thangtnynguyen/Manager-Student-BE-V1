namespace UTEHY.DatabaseCoursePortal.Api.Models.GroupModule
{
    public class GroupModuleDto
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? InvitationCode { get; set; }

        public DateTime? ExpiryTimeInvitation { get; set; }

        public int? Year { get; set; }

        public int? Semester { get; set; }

        public bool? Status { get; set; }

        public int? TeacherId { get; set; }
    }
}
