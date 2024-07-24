using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Question
{
    public class GetQuestionRequest : PagingRequest
    {
        public string? Title { get; set; }

        public int? QuestionCategoryId { get; set; }

        public int? Type { get; set; }

        public int? Difficulty { get; set; }

        public int? SectionId { get; set; }
    }
}
