namespace UTEHY.DatabaseCoursePortal.Api.Models.QuestionCategory
{
    public class CreateQuestionCategoryRequest
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public int? ParentQuestionCategoryId { get; set; }

        public int Priority { get; set; }
    }
}
