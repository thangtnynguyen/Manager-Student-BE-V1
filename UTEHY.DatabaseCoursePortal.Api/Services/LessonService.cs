using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Models.Lesson;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class LessonService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public LessonService(ApplicationDbContext dbContext, IMapper mapper, UserService userService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userService = userService;
        }

        #region Manage lesson
        public async Task<List<Lesson>> GetLessonBySectionId(int sectionId)
        {
            var result = await _dbContext.Lessons.Include(x => x.LessonContents).Where(x => x.SectionId == sectionId).OrderBy(s => s.Priority).ToListAsync();

            return result;
        }

        public async Task<Lesson> Create(CreateLessonRequest request)
        {
            var section = await _dbContext.Sections.FindAsync(request.SectionId);

            if(section is null)
            {
                throw new BadHttpRequestException("Chương không tồn tại trong hệ thống!");
            }

            var lesson = _mapper.Map<Lesson>(request);

            await _userService.AttachCreateInfo(lesson);

            await _dbContext.Lessons.AddAsync(lesson);

            await _dbContext.SaveChangesAsync();

            return lesson;
        }

        public async Task<Lesson> Edit(EditLessonRequest request)
        {
            var lesson = await _dbContext.Lessons.FindAsync(request.Id);

            if (lesson == null)
            {
                throw new BadHttpRequestException("Bài học không tồn tại trong hệ thống!");
            }

            var section = await _dbContext.Sections.FindAsync(request.SectionId);

            if (section is null)
            {
                throw new BadHttpRequestException("Chương không tồn tại trong hệ thống!");
            }

            _mapper.Map(request, lesson);

            await _userService.AttachUpdateInfo(section);

            await _dbContext.SaveChangesAsync();

            return lesson;
        }

        public async Task<Lesson> Delete(int id)
        {
            var lesson = await _dbContext.Lessons.FindAsync(id);

            if (lesson is null)
            {
                throw new ArgumentNullException(nameof(lesson), "Lesson không tồn tại trong hệ thống!");
            }

            await _userService.AttachDeleteInfo(lesson);

            await _dbContext.SaveChangesAsync();

            return lesson;
        }
        #endregion
    }
}
