namespace UTEHY.DatabaseCoursePortal.Api.Models.QuestionAnswer
{
    public class QuestionAnswerDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public bool IsCorrect { get; set; }

        public int Score { get; set; }

        public int QuestionId { get; set; }
    }
}
