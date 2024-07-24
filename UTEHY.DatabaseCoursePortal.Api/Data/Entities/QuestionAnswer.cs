using Newtonsoft.Json;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;

namespace UTEHY.DatabaseCoursePortal.Api.Data.Entities
{
    public class QuestionAnswer : EntityBase
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public bool IsCorrect { get; set; }

        public int Score { get; set; }

        public int QuestionId { get; set; }

        [JsonIgnore]
        public virtual Question Question { get; set; }
    }
}
