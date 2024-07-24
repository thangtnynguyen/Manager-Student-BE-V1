using AutoMapper;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Enums;
using UTEHY.DatabaseCoursePortal.Api.Models.Banner;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Lesson;
using UTEHY.DatabaseCoursePortal.Api.Models.LessonContent;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class LessonContentService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly FileService _fileService;
        private readonly UserService _userService;

        public LessonContentService(ApplicationDbContext dbContext, IMapper mapper, FileService fileService, UserService userService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileService = fileService;
            _userService = userService;
        }

        #region Manage
        public async Task<List<LessonContent>> GetLessonContentByLessonId(int id)
        {
            var result = await _dbContext.LessonContents.Where(x => x.LessonId == id).ToListAsync();

            return result;
        }

        public async Task<LessonContent> Create(LessonContentModel request)
        {
            if(request.Type == (int)TypeLessonContent.File)
            {
                if (request?.File?.Length > 0)
                {
                    request.FileUrl = await _fileService.UploadFileAsync(request.File, PathFolder.LessonContent);
                }
            }

            var lessonContent = _mapper.Map<LessonContent>(request);

            await _userService.AttachCreateInfo(lessonContent);

            await _dbContext.LessonContents.AddAsync(lessonContent);
            await _dbContext.SaveChangesAsync();

            return lessonContent;
        }

        public async Task<LessonContent> Edit(LessonContentModel request)
        {
            var lessonContent = await _dbContext.LessonContents.FindAsync(request.Id);

            if (lessonContent is null)
            {
                throw new BadHttpRequestException("Tài liệu không tồn tại trong hệ thống!");
            }

            var lesson = await _dbContext.Lessons.FindAsync(request.LessonId);

            if (lesson is null)
            {
                throw new BadHttpRequestException("Bài học không tồn tại trong hệ thống!");
            }

            if (request.Type == (int)TypeLessonContent.File)
            {
                if (request?.File?.Length > 0)
                {
                    request.FileUrl = await _fileService.UploadFileAsync(request.File, PathFolder.LessonContent);
                }
            }

            _mapper.Map(request, lessonContent);

            await _userService.AttachUpdateInfo(lessonContent);

            await _dbContext.SaveChangesAsync();

            return lessonContent;
        }

        public async Task<LessonContent> Delete(int id)
        {
            var lessonContent = await _dbContext.LessonContents.FindAsync(id);

            if (lessonContent is null)
            {
                throw new ArgumentNullException(nameof(lessonContent), "LessonContent không tồn tại trong hệ thống!");
            }

            await _userService.AttachDeleteInfo(lessonContent);

            await _dbContext.SaveChangesAsync();

            return lessonContent;
        }
        #endregion

        public async Task<PagingResult<LessonContent>> Get(GetLessonContentRequest request)
        {
            var query = _dbContext.LessonContents.AsQueryable();

            if (request.LessonId.ToString()!=null && !string.IsNullOrEmpty(request.LessonId.ToString()))
            {
                query = query.Where(b => b.LessonId == request.LessonId);
            }

            if (!string.IsNullOrEmpty(request.Title))
            {
                query = query.Where(b => b.Title.ToLower().Contains(request.Title.ToLower()));
            }
            if (!string.IsNullOrEmpty(request.Description))
            {
                query = query.Where(b => b.Description.ToLower().Contains(request.Description.ToLower()));
            }
            int total = await query.CountAsync();

            if (request.PageIndex == null) request.PageIndex = 1;
            if (request.PageSize == null) request.PageSize = total;

            int totalPages = (int)Math.Ceiling((double)total / request.PageSize.Value);

            var items = await query
            .OrderByDescending(b => b.Id)
            .Skip((request.PageIndex.Value - 1) * request.PageSize.Value)
            .Take(request.PageSize.Value)
            .ToListAsync();

            var result = new PagingResult<LessonContent>(items, request.PageIndex.Value, request.PageSize.Value, total, totalPages);

            return result;
        }
    }
}
