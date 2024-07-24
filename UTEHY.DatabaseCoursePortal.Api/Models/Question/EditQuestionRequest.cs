using UTEHY.DatabaseCoursePortal.Api.Models.QuestionAnswer;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Question
{
    public class EditQuestionRequest
    {
        //public int Id { get; set; }

        //public string Title { get; set; }

        //public string? Feedback { get; set; }

        //public int Score { get; set; }

        //public int QuestionCategoryId { get; set; }

        //public List<QuestionAnswerDto>? QuestionAnswers { get; set; }

        //public List<int>? TagIds { get; set; }

        //public int Type { get; set; }

        public int Id { get ; set; }

        public string Title { get; set; }

        public string? Feedback { get; set; }

        public int Score { get; set; }

        public int QuestionCategoryId { get; set; }

        public int Difficulty { get; set; }

        public List<QuestionAnswerDto>? QuestionAnswers { get; set; }

        public List<int>? TagIds { get; set; }

        public int Type { get; set; }

        public int? SectionId { get; set; }
    }
}
