using NSwag.Annotations;
using System.Text.Json.Serialization;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Role
{
    public class CreateRoleRequest
    {

        public string? Name { get; set; }

        public string? Description { get; set; }

        [JsonIgnore]
        public string? NormalizedName {  get; set; }

        public List<int>? PermissionIds { get; set; }



    }
}
