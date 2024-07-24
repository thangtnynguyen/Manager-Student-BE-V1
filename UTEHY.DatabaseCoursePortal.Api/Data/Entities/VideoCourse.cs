using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class VideoCourse : EntityBase
    {
        public int Id { get; set; }
        public string VideoUrl { get; set; }
    }
}
