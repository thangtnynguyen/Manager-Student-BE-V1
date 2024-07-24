using System.ComponentModel.DataAnnotations.Schema;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class Course : EntityBase, IDefaultAttribute
    {
        public int? Id { get; set; }

        public string? Title { get; set; }

        public string? Slug { get; set; }

        public string? ShortDescription { get; set; }

        public string? Content { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsDefault { get; set; }

        #region Relationship
        public virtual List<Section> Sections { get; set; }

        public List<UserCourse>? UserCourses { get; set; }
        #endregion
    }
}
