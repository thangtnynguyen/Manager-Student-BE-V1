namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class UserCourse
    {
        public int? Id { get; set; }

        public Guid? UserId { get; set; }

        public int? CourseId { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public int? Progress { get; set; }

        public int? TimeSpent { get; set; }

        public bool? Status { get; set; }

        public int? LessonContentCurrentId { get; set; }

        public User? User { get; set; }

        public Course? Course { get; set; }
    }
}
