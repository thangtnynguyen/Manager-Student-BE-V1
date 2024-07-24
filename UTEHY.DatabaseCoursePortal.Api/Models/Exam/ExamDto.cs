using UTEHY.DatabaseCoursePortal.Api.Models.Question;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Exam
{
    public class ExamDto
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
        public  List<QuestionDto> Questions { get; set; }
    }
}
