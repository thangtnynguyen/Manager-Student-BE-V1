using Microsoft.AspNetCore.Identity;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string? Name { get; set; }

        public string? AvatarUrl { get; set; }

        public bool? Status { get; set; }

        public int? Sex { get; set; }

        public string? Address { get; set; }

        public DateTime? BirthDay { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        #region Relationship
        public virtual ICollection<Blog> Blogs { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Reaction> Reactions { get; set; }
        public virtual List<UserCourse>? UserCourses { get; set; }

        public virtual List<UserRole>? UserRoles { get; set; }
        #endregion
    }
}
