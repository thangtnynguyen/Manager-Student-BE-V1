using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class ExamResult : EntityBase
    {
        public int? Id { get; set; }

        public int? ExamId { get; set; }

        public int? StudentId { get; set; }

        public int? Score { get; set; }

        public DateTime? StartTime { get; set; }

        public TimeSpan? DurationTime { get; set; }

        public int? NumberCorrectAnswers { get; set; }

        public int? NumberChangeTab { get; set; }

        public virtual Student Student { get; set; }
    }
}
