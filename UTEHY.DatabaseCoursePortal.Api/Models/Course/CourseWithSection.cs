using UTEHY.DatabaseCoursePortal.Api.Models.Section;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Course
{
    public class CourseWithSection
    {
        public string Title { get; set; }

        public List<SectionDto> Sections { get; set; }
    }
}
