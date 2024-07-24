using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Models.QuestionCategory
{
    public class GetQuestionCategoryTreeRequest : PagingRequest
    {
        public List<int>? ParentQuestionCategories { get; set; }

        public string? Name { get; set; }
    }
}
