using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Models.Tag
{
    public class TagDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        public virtual List<QuestionTag> QuestionTags { get; set; }
    }
}
