namespace UTEHY.DatabaseCoursePortal.Api.Models.ExamResult
{
    public class ExamResultDto
    {

        public int? Id { get; set; }

        public int? ExamId { get; set; }

        public string? StudentId { get; set; }

        public string? StudentName { get; set; }

        public int? Score { get; set; }

        public DateTime? StartTime { get; set; }

        public TimeSpan? DurationTime { get; set; }

        public int? NumberCorrectAnswers { get; set; }

        public int? NumberChangeTab { get; set; }

    }
}
