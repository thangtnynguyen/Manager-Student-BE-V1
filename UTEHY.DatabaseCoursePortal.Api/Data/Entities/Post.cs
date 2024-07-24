using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class Post :EntityBase
    {
        public int? Id { get; set; }

        public Guid? UserId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Image { get; set; }

        public string? Content { get; set; }

        public int? Priority { get; set; }

        public bool? IsPublished { get; set; }

        public bool? IsApproved { get; set; }

        public User? User { get; set; }

        public List<Comment>? Comments { get; set; }
    }
}
