using System.ComponentModel.DataAnnotations;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class Reaction : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public int TypeReactable { get; set; }
        public string Type { get; set; }
        public int? BlogId { get; set; }
        public int? CommentId { get; set; }
    }
}
