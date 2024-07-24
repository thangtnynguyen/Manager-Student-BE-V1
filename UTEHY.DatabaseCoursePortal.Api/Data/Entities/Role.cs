using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public string? Description { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? DeletedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        [JsonIgnore]
        public List<RolePermission> RolePermissions { get; set; }
    }
}
