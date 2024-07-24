using UTEHY.DatabaseCoursePortal.Api.Controllers.Admin;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Post;
using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Models.QuestionCategory;
using UTEHY.DatabaseCoursePortal.Api.Models.Comment;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class CommentMapper:Profile
    {
         public CommentMapper()
         {
            CreateMap<Comment, CommentDto>();
         }
    }
}
