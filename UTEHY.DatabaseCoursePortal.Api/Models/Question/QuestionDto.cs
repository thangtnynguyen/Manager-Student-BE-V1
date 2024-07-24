using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.QuestionAnswer;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Question
{
    public class QuestionDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Feedback { get; set; }

        public int Score { get; set; }

        public int? SectionId { get; set; }

        public int QuestionCategoryId { get; set; }

        public int Difficulty { get; set; }

        public int Type { get; set; }

        public string QuestionCategoryName { get; set; }

        public Data.Entities.Section Section { get; set; }

        public List<QuestionAnswerDto> QuestionAnswers { get; set; }

        public List<QuestionTag> QuestionTags { get; set; }

        public List<Data.Entities.Tag> Tags { get; set; }
        public List<QuestionAnswerDto> Answers { get; internal set; }
    }
}
