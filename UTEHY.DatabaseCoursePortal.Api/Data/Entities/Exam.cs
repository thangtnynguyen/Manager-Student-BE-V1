using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class Exam: EntityBase
    {
        public int? Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public TimeSpan? Duration { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public bool? IsShowContent { get; set; }

        public bool? IsSeeScore { get; set; }

        public bool? IsMixQuestion { get; set; }

        public bool? IsMixQuestionAnswer { get; set; }

        public bool? IsAllowChangeTab { get; set; }

        public bool? Status { get; set; }

        public int? Type { get; set; }

        public int? NumberQuestionDifficult { get; set; }

        public int? NumberQuestionModerate { get; set; }

        public int? NumberQuestionEasy { get; set; }

        public virtual List<ExamQuestion> ExamQuestions { get; set; }

        public virtual List<ExamResult> ExamResults { get; set; }

        public virtual List<ExamGroupModule> ExamGroupModules { get; set; }

    }
}
