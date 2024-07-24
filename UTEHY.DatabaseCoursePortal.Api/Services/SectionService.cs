using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Exceptions;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Section;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class SectionService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserService _userService;
        private readonly CourseService _courseService;
        private readonly IMapper _mapper;

        public SectionService(ApplicationDbContext dbContext, IMapper mapper, UserService userService, CourseService courseService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userService = userService;
            _courseService = courseService;
        }

        #region Manage section
        public async Task<List<Section>> GetAll()
        {
            var sections = await _dbContext.Sections
               .Include(s => s.UserCreated)
               .Include(s => s.UserUpdated)
               .OrderBy(s => s.Priority)
               .ToListAsync();

            return sections;
        }

        public async Task<Section> GetSectionWithLesson(int id)
        {
            var section = await _dbContext.Sections
               .Where(s => s.Id == id)
               .Include(s => s.Lessons.OrderBy(l => l.Priority))
               .ThenInclude(x => x.LessonContents)
               .FirstOrDefaultAsync();

            if (section is null)
            {
                throw new BadHttpRequestException("Chương không tồn tại trong hệ thống!");
            }

            return section;
        }

        public async Task<Section> Create(CreateSectionRequest request)
        {
            var section = _mapper.Map<Section>(request);

            var course = await _courseService.GetCourse();

            section.CourseId = course.Id;

            await _userService.AttachCreateInfo(section);

            await _dbContext.Sections.AddAsync(section);

            await _dbContext.SaveChangesAsync();

            return section;
        }

        public async Task<Section> Edit(EditSectionRequest request)
        {
            var section = await _dbContext.Sections.FindAsync(request.Id);

            if (section is null)
            {
                throw new ArgumentNullException(nameof(section), "Không thể tìm thấy section!");
            }

            _mapper.Map(request, section);

            await _userService.AttachUpdateInfo(section);

            await _dbContext.SaveChangesAsync();

            return section;
        }

        public async Task<Section> Delete(int id)
        {
            var section = await _dbContext.Sections.FindAsync(id);

            if (section is null)
            {
                throw new ArgumentNullException(nameof(section), "Không thể tìm thấy section!");
            }

            await _userService.AttachDeleteInfo(section);

            await _dbContext.SaveChangesAsync();

            return section;
        }
        #endregion
    }
}
