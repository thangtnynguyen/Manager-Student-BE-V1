namespace UTEHY.DatabaseCoursePortal.Api.Models.Comment
{
    public class RequestCommentBlogViewModel
    {
        public string Content { get; set; }
        public string TypeCommentable { get; set; }
        public int BlogId { get; set; }
        public int? ParentCommentId { get; set; }
    }
}
