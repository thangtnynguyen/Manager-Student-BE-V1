using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Configurations;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Initializers;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Seeders;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Banner> Banners { get; set; } = null!;
        public virtual DbSet<Blog>? Blogs { get; set; }
        public virtual DbSet<Post>? Posts { get; set; }
        public virtual DbSet<BlogTopic>? BlogTopics { get; set; }
        public virtual DbSet<Comment>? Comments { get; set; }
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<VideoCourse>? VideoCourses { get; set; }
        //public virtual DbSet<TrackCourse>? TrackCourses { get; set; }
        public virtual DbSet<Permission>? Permissions { get; set; }
        public virtual DbSet<RolePermission>? RolePermissions { get; set; }
        public virtual DbSet<Config>? Configs { get; set; } = null!;
        public virtual DbSet<UserCourse> UserCourses { get; set; } = null!;
        public virtual DbSet<Page> Pages { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;
        public virtual DbSet<Track> Tracks { get; set; } = null!;

        public virtual DbSet<AssignmentCourse> AssignmentCourses { get; set; } = null!;

        public virtual DbSet<QuizCourse> QuizCourses { get; set; } = null!;
        public virtual DbSet<TypeStepTrack> TypeStepTracks { get; set; } = null!;
        public virtual DbSet<StepTrack> StepTracks { get; set; } = null!;
        public virtual DbSet<Question> Questions { get; set; } = null!;
        public virtual DbSet<QuestionCategory> QuestionCategories { get; set; } = null!;
        public virtual DbSet<QuestionAnswer> QuestionAnswers { get; set; } = null!;
        public virtual DbSet<QuestionTag> QuestionTags { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<Lesson> Lessons { get; set; } = null!;
        public virtual DbSet<LessonContent> LessonContents { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<UserGroup> UserGroups { get; set; } = null!;
        public virtual DbSet<Exam> Exams { get; set; } = null!;
        public virtual DbSet<ExamQuestion> ExamQuestions { get; set; } = null!;
        public virtual DbSet<GroupModule> GroupModules { get; set; } = null!;
        public virtual DbSet<StudentGroupModule> StudentGroupModules { get; set; } = null!;
        public virtual DbSet<ExamGroupModule> ExamGroupModules { get; set; } = null!;
        public virtual DbSet<ExamResult> ExamResults { get; set; } = null!;
        public virtual DbSet<Notification> Notifications { get; set; } = null!;
        public virtual DbSet<NotificationGroupModule> NotificationGroupModules { get; set; } = null!;
        public virtual DbSet<Section> Sections { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<Attendance> Attendances { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<User>()
            .HasMany(u => u.UserRoles)
            .WithOne()
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();




            builder.Entity<User>()
                .HasMany(u => u.UserRoles)
                .WithOne()
                .HasForeignKey(ur => ur.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);  // Chỉ định rằng không có xóa cascade

            //builder.Entity<IdentityUserRole<Guid>>()
            //    .HasKey(ur => new { ur.UserId, ur.RoleId });

            //builder.Entity<IdentityUserRole<Guid>>()
            //    .HasOne<IdentityRole<Guid>>()
            //    .WithMany()
            //    .HasForeignKey(ur => ur.RoleId)
            //    .IsRequired()
            //    .OnDelete(DeleteBehavior.Restrict);

            //Configuration
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RoleConfiguration());
            builder.ApplyConfiguration(new BannerConfiguration());
            builder.ApplyConfiguration(new PermissionConfiguration());
            builder.ApplyConfiguration(new RolePermissionConfiguration());
            builder.ApplyConfiguration(new BlogConfiguration());
            builder.ApplyConfiguration(new BlogTopicConfiguration());
            builder.ApplyConfiguration(new ConfigConfiguration());
            builder.ApplyConfiguration(new UserCourseConfiguration());
            builder.ApplyConfiguration(new PageConfiguration());
            builder.ApplyConfiguration(new TrackConfiguration());
            builder.ApplyConfiguration(new AssignmentCourseConfiguration());
            builder.ApplyConfiguration(new QuizCourseConfiguration());
            builder.ApplyConfiguration(new TypeStepTrackConfiguration());
            builder.ApplyConfiguration(new StepTrackConfiguration());
            builder.ApplyConfiguration(new QuestionConfiguration());
            builder.ApplyConfiguration(new QuestionCategoryConfiguration());
            builder.ApplyConfiguration(new QuestionAnswerConfiguration());
            builder.ApplyConfiguration(new TagConfiguration());
            builder.ApplyConfiguration(new QuestionTagConfiguration());
            builder.ApplyConfiguration(new LessonConfiguration());
            builder.ApplyConfiguration(new LessonContentConfiguration());
            builder.ApplyConfiguration(new GroupConfiguration());
            builder.ApplyConfiguration(new UserGroupConfiguration());
            builder.ApplyConfiguration(new ExamConfiguration());
            builder.ApplyConfiguration(new ExamQuestionConfiguration());
            builder.ApplyConfiguration(new GroupModuleConfiguration());
            builder.ApplyConfiguration(new StudentGroupModuleConfiguration());
            builder.ApplyConfiguration(new ExamGroupModuleConfiguration());
            builder.ApplyConfiguration(new ExamResultConfiguration());
            builder.ApplyConfiguration(new NotificationConfiguration());
            builder.ApplyConfiguration(new NotificationGroupModuleConfiguration());
            builder.ApplyConfiguration(new SectionConfiguration());
            builder.ApplyConfiguration(new ScheduleConfiguration());
            builder.ApplyConfiguration(new AttendanceConfiguration());

            //Entity
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
            builder.Entity<Banner>().HasQueryFilter(x => x.DeletedAt == null);
            builder.Entity<Blog>().HasQueryFilter(x => x.DeletedAt == null);
            builder.Entity<BlogTopic>().HasQueryFilter(x => x.DeletedAt == null);
            builder.Entity<Comment>().HasQueryFilter(x => x.DeletedAt == null);

            //Seeder 
            //builder.Seed();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
