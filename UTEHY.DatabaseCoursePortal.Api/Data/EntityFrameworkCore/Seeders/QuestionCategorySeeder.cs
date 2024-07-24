using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Seeders
{
    public class QuestionCategorySeeder
    {
        public static List<QuestionCategory> Data()
        {
            var records = new List<QuestionCategory>()
            {
                new QuestionCategory
                {
                    Id = 1,
                    Name = "Mặc định",
                    Description = "Thư mục loại câu hỏi mặc định",
                    ParentQuestionCategoryId = null,
                    IsDefault = true,
                    Priority = 0,
                }
            };

            return records;
        }
    }
}
