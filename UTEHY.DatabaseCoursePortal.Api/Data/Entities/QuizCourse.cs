using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class QuizCourse : EntityBase
    {
        public int Id { get; set; }
        public int AssignmentCourseId { get; set; }
        public string Answer { get; set; }
        public string Explanation { get; set; }
        public bool IsCorrectAnswer { get; set; }
    }
}
