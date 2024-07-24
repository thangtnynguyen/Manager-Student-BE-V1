using UTEHY.DatabaseCoursePortal.Api.Models.Common;

namespace UTEHY.DatabaseCoursePortal.Api.Models.QuestionCategory
{
    public class GetQuestionCategoryRequest : PagingRequest
    {
        public string? Name { get; set; }
    }
}
