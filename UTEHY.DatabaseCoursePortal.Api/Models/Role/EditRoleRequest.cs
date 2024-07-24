using NSwag.Annotations;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Role
{
    public class EditRoleRequest
    {
        public Guid? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        [SwaggerIgnore]
        public string? NormalizedName {  get; set; }



    }
}
