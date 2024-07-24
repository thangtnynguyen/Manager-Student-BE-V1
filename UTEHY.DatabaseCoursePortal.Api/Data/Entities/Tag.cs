using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class Tag : EntityBase
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }

        //public virtual List<QuestionTag> QuestionTags { get; set; }
    }
}
