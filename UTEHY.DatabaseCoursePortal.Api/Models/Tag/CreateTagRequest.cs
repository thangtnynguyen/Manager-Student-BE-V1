namespace UTEHY.DatabaseCoursePortal.Api.Models.Tag
{
    public class CreateTagRequest
    {
        public string Name { get;set; }

        /// <summary>
        /// 1.Câu hỏi 2.Bài viết
        /// </summary>
        /// 
        public int Type { get; set; }
    }
}
