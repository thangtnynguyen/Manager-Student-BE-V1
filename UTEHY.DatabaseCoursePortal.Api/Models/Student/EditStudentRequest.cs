namespace UTEHY.DatabaseCoursePortal.Api.Models.Student
{
    public class EditStudentRequest
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public bool? Status { get; set; }

        public string? StudentId { get; set; }
    }
}
