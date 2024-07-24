using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Exceptions;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Course;
using UTEHY.DatabaseCoursePortal.Api.Models.Track;
using UTEHY.DatabaseCoursePortal.Api.Models.Section;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class CourseService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        private readonly FileService _fileService;

        public CourseService(ApplicationDbContext dbContext, IMapper mapper, FileService fileService,UserService userService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _fileService = fileService;
            _userService = userService;
        }

        #region Manage Course
        public async Task<Course> GetCourse()
        {
            var course = await _dbContext.Courses.FirstOrDefaultAsync(x => x.IsDefault == true);

            if(course is null)
            {
                throw new ArgumentNullException("Thông tin khoá học Database không tồn tại!");
            }

            return course;
        }

        public async Task<Course> EditCourse(EditCourseRequest request)
        {
            Course course = await this.GetCourse();

            _mapper.Map(request, course);

            course.UpdatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return course;
        }
        #endregion

        #region Manage Section
        public async Task<CourseWithSection> GetCourseWithSections()
        {
            var course = await _dbContext.Courses
                .Include(c => c.Sections)
                .ThenInclude(s => s.UserCreated)
                .Include(c => c.Sections)
                .ThenInclude(s => s.UserUpdated)
                .FirstOrDefaultAsync(x => x.IsDefault == true);

            var result = _mapper.Map<CourseWithSection>(course);

            if (result is null)
            {
                throw new ArgumentNullException("Thông tin khoá học Database không tồn tại!");
            }

            return result;
        }

        public async Task<Section> CreateSection(CreateSectionRequest request)
        {
            var section = _mapper.Map<Section>(request);

            var course = await GetCourse();

            section.CourseId = course.Id;

            await _userService.AttachCreateInfo(section);

            await _dbContext.Sections.AddAsync(section);

            await _dbContext.SaveChangesAsync();

            return section;
        }
        #endregion

        //public async Task<Course> GetDatabaseCourse()
        //{
        //    try
        //    {
        //        var course = await _dbContext.Courses
        //            .Include(c => c.Lessons)
        //            .ThenInclude(l => l.LessonContents)
        //            .FirstOrDefaultAsync(x => x.IsDefault == true);
        //        return course;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
        //    }
        //}

        public async Task<Course> EditDatabaseCourse(EditDatabaseCourseRequest request)
        {
            try
            {
                var course = await _dbContext.Courses.FirstOrDefaultAsync(x => x.IsDefault == true);

                if (course == null)
                {
                    throw new ApiException("Không tìm thấy khoá học hợp lệ!", HttpStatusCode.InternalServerError, null);
                }

                _mapper.Map(request, course);

                course.UpdatedAt = DateTime.Now;

                await _dbContext.SaveChangesAsync();

                return course;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<List<Course>> GetCourses()
        {
            var listCourses = await _dbContext.Courses.ToListAsync();

            return listCourses;
        }
        public async Task<Course> GetCourseById(int id)
        {
            var course = await _dbContext.Courses.FindAsync(id);


            return course;
        }
        //public async Task<List<VideoCourse>> GetListVideoCourseByIdCourse(int courseId)
        //{
        //    var listVideoCourse = await _dbContext.VideoCourses.Where(x => x.CourseId == courseId).ToListAsync();

        //    return listVideoCourse;
        //}
        public async Task<Course> CreateCourse(CreateCourseRequest request)
        {
            if (request.ImageFile?.Length > 0)
            {
                request.ImageUrl = await _fileService.UploadFileAsync(request.ImageFile, PathFolder.Course);
            }
            var course = _mapper.Map<Course>(request);

            if (request.PublishedAt < DateTime.Now)
            {
                request.IsPublished = true;
            }

            await _dbContext.Courses.AddAsync(course);
            await _dbContext.SaveChangesAsync();

            return course;
        }

        public async Task<Track> CreateTrackByCourseId(CreateTrackRequest request)
        {
            var track = _mapper.Map<Track>(request);

            await _dbContext.Tracks.AddAsync(track);
            await _dbContext.SaveChangesAsync();

            return track;
        }

        public async Task<StepTrack> CreateStepTrackByTrackId(CreateStepTrackRequest request)
        {
            var stepTrack = _mapper.Map<StepTrack>(request);

            if (request.VideoCourse != null)
            {
                var videoCourse = _mapper.Map<VideoCourse>(request.VideoCourse);
                await _dbContext.VideoCourses.AddAsync(videoCourse);
                await _dbContext.SaveChangesAsync();
                stepTrack.VideoCourseId = videoCourse.Id;
            }
            if (request.AssignmentCourse != null)
            {
                //var quizCourse = _mapper.Map<QuizCourse>(request.AssignmentCourse.QuizCourses);
                //await _dbContext.QuizCourses.AddAsync(quizCourse);
                //await _dbContext.SaveChangesAsync();
                //stepTrack.AssignmentCourseId = quizCourse.Id;
                var assigment = await CreateAssignment(request.AssignmentCourse);
                stepTrack.AssignmentCourseId = assigment.Id;
            }

            await _dbContext.StepTracks.AddAsync(stepTrack);
            await _dbContext.SaveChangesAsync();
            return stepTrack;
        }
        public async Task<PagingResult<Course>> GetCourses(PagingRequest request)
        {
            var query = _dbContext.Courses.AsQueryable();

            int total = await query.CountAsync();

            if (request.PageIndex == null) request.PageIndex = 1;
            if (request.PageSize == null) request.PageSize = total;

            int totalPages = (int)Math.Ceiling((double)total / request.PageSize.Value);

            var courses = await query.Skip((request.PageIndex.Value - 1) * request.PageSize.Value)
                                .Take(request.PageSize.Value)
                                .ToListAsync();

            var result = new PagingResult<Course>(courses, request.PageIndex.Value, request.PageSize.Value, total, totalPages);

            return result;
        }

        public async Task<IEnumerable<Track>> GetTracksByCourseId(int courseId)
        {
            var tracks = _dbContext.Tracks.Where(x => x.CourseId == courseId).ToList();

            return tracks;
        }

        public async Task<IEnumerable<CreateStepTrackRequest>> GetStepTracksByTrackId(int trackId)
        {
            var stepTrack = await _dbContext.StepTracks.Where(x => x.TrackID == trackId).ToListAsync();
            var stepTrackVM = new List<CreateStepTrackRequest>();
            for (int i = 0; i < stepTrack.Count; i++)
            {
                stepTrackVM.Add(_mapper.Map<CreateStepTrackRequest>(stepTrack[i]));
                if (stepTrack[i].VideoCourseId != null)
                {
                    var videoCourse = await _dbContext.VideoCourses.FirstOrDefaultAsync(x => x.Id == stepTrack[i].VideoCourseId);
                    stepTrackVM[i].VideoCourse = videoCourse;
                }
                if (stepTrack[i].AssignmentCourseId != null)
                {
                    var assignmentCourse = await _dbContext.AssignmentCourses.FirstOrDefaultAsync(x => x.Id == stepTrack[i].AssignmentCourseId);
                    var quizCoursesByAssignmentCourseId = await _dbContext.QuizCourses.Where(x => x.AssignmentCourseId == stepTrack[i].AssignmentCourseId).ToListAsync();
                    var quizCourses = new List<QuizCourseRequest>();
                    for (int j = 0; i < quizCoursesByAssignmentCourseId.Count; j++)
                    {
                        quizCourses.Add(_mapper.Map<QuizCourseRequest>(quizCoursesByAssignmentCourseId[i]));
                    }
                    //var quizCourses = _mapper.Map<QuizCourseRequest>(quizCoursesByAssignmentCourseId);
                    var assignmentCourserequest = new CreateAssignmentCourseRequest
                    {
                        Title = assignmentCourse.Title,
                        Question = assignmentCourse.Question,
                        QuizCourses = quizCourses
                    };
                    stepTrackVM[i].AssignmentCourse = assignmentCourserequest;

                }
            }
            return stepTrackVM;
        }

        public async Task<AssignmentCourse> CreateAssignment(CreateAssignmentCourseRequest request)
        {
            var assignmentCourse = new AssignmentCourse
            {
                Title = request.Title,
                Question = request.Question
            };

            await _dbContext.AssignmentCourses.AddAsync(assignmentCourse);
            await _dbContext.SaveChangesAsync();

            if (request.QuizCourses != null)
            {
                for (int i = 0; i < request.QuizCourses.Count; i++)
                {
                    var quizCourse = _mapper.Map<QuizCourse>(request.QuizCourses[i]);
                    quizCourse.AssignmentCourseId = assignmentCourse.Id;

                    await _dbContext.QuizCourses.AddAsync(quizCourse);
                }
            }

            await _dbContext.SaveChangesAsync();

            return assignmentCourse;
        }

        #region Course User
        //public async Task<CourseLearningUser> GetCourseLearningUser(GetCourseLearningUserRequest request, HttpContext httpContext)
        //{
        //    try
        //    {
        //        var course = await _dbContext.Courses
        //            .Include(c => c.Lessons)
        //            .ThenInclude(l => l.LessonContents)
        //            .FirstOrDefaultAsync(x => x.Slug == request.Slug);

        //        if(course == null)
        //        {
        //            throw new ApiException("Không tìm thấy khoá học hợp lệ!", HttpStatusCode.InternalServerError);
        //        }

        //        var result = _mapper.Map<CourseLearningUser>(course);

        //        if(httpContext.User.Claims.FirstOrDefault() != null)
        //        {
        //            var userName = httpContext.User.Claims.First(x => x.Type == "UserName").Value;

        //            if (userName != null)
        //            {
        //                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);

        //                if (user != null)
        //                {
        //                    var isRegister = await _dbContext.UserCourses.FirstOrDefaultAsync(x => x.UserId == user.Id && x.CourseId == course.Id);

        //                    result.IsRegister = isRegister != null ? true : false;
        //                }
        //            }
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
        //    }
        //}

        //public async Task<bool> RegisterCourseStudent(RegisterCourseStudentRequest request, HttpContext httpContext)
        //{
        //    try
        //    {
        //        var course = await _dbContext.Courses
        //            .Include(c => c.Lessons)
        //            .ThenInclude(l => l.LessonContents)
        //            .FirstOrDefaultAsync(x => x.Slug == request.Slug);

        //        if (course == null)
        //        {
        //            throw new ApiException("Không tìm thấy khoá học hợp lệ!", HttpStatusCode.InternalServerError);
        //        }

        //        if (httpContext.User.Claims.FirstOrDefault() == null)
        //        {
        //            throw new ApiException("Thông tin người dùng đăng nhập không hợp lệ!", HttpStatusCode.Unauthorized);
        //        }

        //        var userName = httpContext.User.Claims.First(x => x.Type == "UserName").Value;

        //        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);

        //        var isRegister = await _dbContext.UserCourses.FirstOrDefaultAsync(x => x.UserId == user.Id && x.CourseId == course.Id);

        //        if(isRegister != null) 
        //        {
        //            throw new ApiException("Người dùng đã đăng ký khoá học!", HttpStatusCode.BadRequest);
        //        }

        //        var contentLessonId = course.Lessons?.FirstOrDefault()?.LessonContents?.FirstOrDefault()?.Id;

        //        var userCourse = new UserCourse()
        //        {
        //            UserId = user?.Id,
        //            CourseId = course.Id,
        //            LessonContentCurrentId = contentLessonId
        //        };

        //        userCourse.RegistrationDate = DateTime.Now;

        //        await _dbContext.UserCourses.AddAsync(userCourse);
        //        await _dbContext.SaveChangesAsync();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
        //    }
        //}
        #endregion
    }
}
