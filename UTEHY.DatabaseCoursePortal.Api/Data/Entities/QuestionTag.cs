namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class QuestionTag
    {
        public int Id { get; set; }

        public int TagId { get; set; }

        public int QuestionId { get; set; }

        //public virtual Question Question { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
