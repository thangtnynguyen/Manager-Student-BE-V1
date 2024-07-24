namespace UTEHY.DatabaseCoursePortal.Api.Models.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string TypeCommentable { get; set; }
        public int? CommentsCount { get; set; }
        public int? BlogId { get; set; }
        public int? VideoCourseId { get; set; }
        public int? PostId { get; set; }
        public int? ParentCommentId { get; set; }
        public bool IsAnswered { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsReacted { get; set; }
        public bool? IsRemoved { get; set; }
        public bool? IsVoted { get; set; }
        public int? VotersCount { get; set; }
    }
}
