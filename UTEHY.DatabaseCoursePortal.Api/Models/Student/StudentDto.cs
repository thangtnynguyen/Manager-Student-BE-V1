namespace UTEHY.DatabaseCoursePortal.Api.Models.Student
{
    public class StudentDto
    {
        public int? Id { get; set; }

        public Guid? UserId { get; set; }

        public string? AvatarUrl { get; set; }

        public bool? Status { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? StudentId { get; set; }
    }
}
