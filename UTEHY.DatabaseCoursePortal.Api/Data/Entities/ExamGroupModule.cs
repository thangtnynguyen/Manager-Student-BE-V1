namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class ExamGroupModule
    {
        public int? Id { get; set; }

        public int? GroupModuleId { get; set; }

        public int? ExamId { get; set; }

        public virtual Exam Exam { get; set; }
    }
}
