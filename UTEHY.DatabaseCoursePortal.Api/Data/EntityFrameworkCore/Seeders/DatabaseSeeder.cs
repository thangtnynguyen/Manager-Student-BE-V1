using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Seeders
{
    public static class DatabaseSeeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(RoleSeeder.Data());
            modelBuilder.Entity<User>().HasData(UserSeeder.Data());
            modelBuilder.Entity<UserRole>().HasData(UserRoleSeeder.Data());
            modelBuilder.Entity<Course>().HasData(CourseSeeder.Data());
            modelBuilder.Entity<Config>().HasData(ConfigSeeder.Data());
            modelBuilder.Entity<Banner>().HasData(BannerSeeder.Data());
            modelBuilder.Entity<UserCourse>().HasData(UserCourseSeeder.Data());
            modelBuilder.Entity<Page>().HasData(PageSeeder.Data());
            modelBuilder.Entity<Post>().HasData(PostSeeder.Data());
            modelBuilder.Entity<Teacher>().HasData(TeacherSeeder.Data());
            modelBuilder.Entity<Student>().HasData(StudentSeeder.Data());
            modelBuilder.Entity<Comment>().HasData(CommentSeeder.Data());
            modelBuilder.Entity<QuestionCategory>().HasData(QuestionCategorySeeder.Data());
        }
    }
}
