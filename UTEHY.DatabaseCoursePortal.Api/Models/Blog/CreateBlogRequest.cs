using System.Text.Json.Serialization;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Blog
{
    public class CreateBlogRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public IFormFile Image { get; set; }
        [JsonIgnore]
        public string? ImageUrl { get; set; }
        public bool IsApproved { get; set; }
        public bool IsBookmark { get; set; }
        public bool IsPublished { get; set; }
        public string Slug { get; set; }
        public int? ParentId { get; set; }
        public DateTime PulishedAt { get; set; }
    }
}
