namespace UTEHY.DatabaseCoursePortal.Api.Models.GoogleClould
{
    public class VideoYoutube
    {
        public string? Id { get; set; }

        public string? UrlVideo { get; set; }

        public string? Title { get; set; }

        public string? ThumbnailUrl { get; set; }

        public ulong? ViewCount { get; set; }

        public ulong? LikeCount { get; set; }

        public ulong? CommentCount { get; set; }

        public string? Duration { get; set; }
    }
}
