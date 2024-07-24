using System.Runtime.CompilerServices;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

namespace UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore.Seeders
{
    public class CommentSeeder
    {

        public static List<Comment> Data()
        {
            var posts = new List<Comment>()
            {
                new Comment
                {
                    Id = 1,
                    Content="Học đi nào SGP",
                    TypeCommentable="",
                    CommentsCount=1,
                    BlogId=1,
                    VideoCourseId=1,
                    PostId=1,
                    //ParentCommentId=1,
                    IsAnswered=true,
                    IsApproved=true,
                    IsReacted=true,
                    IsRemoved=false,
                    IsVoted=false,
                    VotersCount=1,
                },
                new Comment
                {
                    Id = 2,
                    Content="Học đi nào VGM",
                    TypeCommentable="",
                    CommentsCount=1,
                    BlogId=1,
                    VideoCourseId=1,
                    PostId=1,
                    ParentCommentId=1,
                    IsAnswered=true,
                    IsApproved=true,
                    IsReacted=true,
                    IsRemoved=false,
                    IsVoted=false,
                    VotersCount=1,
                },
                new Comment
                {
                    Id = 3,
                    Content="Học đi nào GGL",
                    TypeCommentable="",
                    CommentsCount=1,
                    BlogId=1,
                    VideoCourseId=1,
                    PostId=2,
                    ParentCommentId=1,
                    IsAnswered=true,
                    IsApproved=true,
                    IsReacted=true,
                    IsRemoved=false,
                    IsVoted=false,
                    VotersCount=1,
                },
                new Comment
                {
                    Id = 4,
                    Content="Học đi nào FL",
                    TypeCommentable="",
                    CommentsCount=1,
                    BlogId=1,
                    VideoCourseId=1,
                    PostId=3,
                    //ParentCommentId=1,
                    IsAnswered=true,
                    IsApproved=true,
                    IsReacted=true,
                    IsRemoved=false,
                    IsVoted=false,
                    VotersCount=1,
                },
                 new Comment
                 {
                    Id = 5,
                    Content="Học đi nào TLN",
                    TypeCommentable="",
                    CommentsCount=1,
                    BlogId=1,
                    VideoCourseId=1,
                    PostId=1,
                    ParentCommentId=1,
                    IsAnswered=true,
                    IsApproved=true,
                    IsReacted=true,
                    IsRemoved=false,
                    IsVoted=false,
                    VotersCount=1,
                 }

            };

            return posts;
        }
    }
}
