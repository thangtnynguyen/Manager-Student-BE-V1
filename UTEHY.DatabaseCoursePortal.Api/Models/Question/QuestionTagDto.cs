namespace UTEHY.DatabaseCoursePortal.Api.Models.Question
{
    public class QuestionTagDto
    {
        public int Id { get; set; }

        public int TagId { get; set; }

        public int QuestionId { get; set; }

        public TagDto Tag { get; set; }
    }

    public class TagDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Type { get; set; }
    }
}
