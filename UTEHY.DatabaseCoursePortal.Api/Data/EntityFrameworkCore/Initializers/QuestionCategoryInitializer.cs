using Microsoft.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Initializers
{
    public class QuestionCategoryInitializer
    {
        public static void Initialize(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<QuestionCategory>().HasData(new QuestionCategory 
            {
                Id = 1,
                Name = "Mặc định",
                Description = "Thư mục loại câu hỏi mặc định",
                ParentQuestionCategoryId = null,
                IsDefault = true,
                Priority = 0,
            });
        }
    }
}
