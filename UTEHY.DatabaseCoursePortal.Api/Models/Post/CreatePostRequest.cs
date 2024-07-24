using System.Text.Json.Serialization;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Post
{
    public class CreatePostRequest
    {

        public int? Id { get; set; }

        public Guid? UserId { get; set; }

        public IFormFile? ImageFile { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        [JsonIgnore]
        public string? Image { get; set; }

        public string? Content { get; set; }

        public int? Priority { get; set; }

        public bool? IsPublished { get; set; }
    }
}
