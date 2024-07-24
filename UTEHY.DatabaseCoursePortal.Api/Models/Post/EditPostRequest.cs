using System.Text.Json.Serialization;
using UTEHY.DatabaseCoursePortal.Api.Models.Comment;
using UTEHY.DatabaseCoursePortal.Api.Models.User;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Post
{
    public class EditPostRequest
    {
        public int? Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public IFormFile? ImageFile { get; set; }

        [JsonIgnore]
        public string? Image { get; set; }

        public string? Content { get; set; }

        public bool? IsPublished { get; set; }

        public bool? IsApproved { get; set;}

    }
}
