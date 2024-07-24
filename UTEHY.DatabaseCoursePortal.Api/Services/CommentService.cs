using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class CommentService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public CommentService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        //public async Task<List<Comment> 




    }
}
