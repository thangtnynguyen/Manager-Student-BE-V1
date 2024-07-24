using System.ComponentModel.DataAnnotations.Schema;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class Section : EntityBase
    {
        public int? Id { get; set; }

        public int? CourseId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public int? Priority { get; set; }

        public bool? Status { get; set; }

        #region Relationship
        [ForeignKey("CreatedBy")]
        public virtual User? UserCreated { get; set; }

        [ForeignKey("UpdatedBy")]
        public virtual User? UserUpdated { get; set; }

        [ForeignKey("DeletedBy")]
        public virtual User? UserDeleted { get; set; }

        public virtual List<Lesson> Lessons { get; set; }
        #endregion
    }
}
