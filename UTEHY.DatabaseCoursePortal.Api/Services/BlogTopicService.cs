using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Models.BlogTopic;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class BlogTopicService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public BlogTopicService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<BlogTopic>> Get()
        {
            var listBlogTopic = _dbContext.BlogTopics.ToList();

            return listBlogTopic;
        }

        public async Task<BlogTopic> Create(RequestBlogTopicViewModel request)
        {
            var blogTopic = _mapper.Map<BlogTopic>(request);

            blogTopic.CreatedAt = DateTime.Now;

            await _dbContext.BlogTopics.AddAsync(blogTopic);
            await _dbContext.SaveChangesAsync();

            return blogTopic;
        }

        public async Task<BlogTopic> Edit(RequestBlogTopicViewModel request, int id)
        {
            var blogTopic = await _dbContext.BlogTopics.FindAsync(id);

            if (blogTopic != null)
            {
                _mapper.Map(request, blogTopic, opt =>
                {
                    opt.Items["Request"] = request;
                });

                blogTopic.UpdatedAt = DateTime.Now;

                await _dbContext.SaveChangesAsync();
            }

            return blogTopic;
        }

        public async Task<BlogTopic> Delete(int id)
        {
            var blogTopic = await _dbContext.BlogTopics.FindAsync(id);
            blogTopic.DeletedAt = DateTime.Now;

            //_deleteEntity.Delete(typeof(BlogTopic), id);

            _dbContext.BlogTopics.Remove(blogTopic);

            await _dbContext.SaveChangesAsync();

            return blogTopic;
        }
    }
}
