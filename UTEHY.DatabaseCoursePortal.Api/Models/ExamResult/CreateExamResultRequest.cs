namespace UTEHY.DatabaseCoursePortal.Api.Models.ExamResult
{
    public class CreateExamResultRequest
    {

        public int? ExamId { get; set; }

        public string? StudentId { get; set; }

        public int? Score { get; set; }

        public DateTime? StartTime { get; set; }

        public TimeSpan? DurationTime { get; set; }

        public int? NumberCorrectAnswers { get; set; }

        public int? NumberChangeTab { get; set; }

    }
}
