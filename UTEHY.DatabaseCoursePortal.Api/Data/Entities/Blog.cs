using System.ComponentModel.DataAnnotations;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class Blog : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? ImageUrl { get; set; }
        public bool? IsApproved { get; set; }
        public bool? IsBookmark { get; set; }
        public bool? IsPublished { get; set; }
        public string Slug { get; set; }
        public int? ParentId { get; set; }
        public DateTime PulishedAt { get; set; }
        public int BlogTopicId { get; set; }
    }
}
