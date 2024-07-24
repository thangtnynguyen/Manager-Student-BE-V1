using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using Twilio.Http;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Enums;
using UTEHY.DatabaseCoursePortal.Api.Exceptions;
using UTEHY.DatabaseCoursePortal.Api.Models.Banner;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Mail;
using UTEHY.DatabaseCoursePortal.Api.Models.Teacher;
using UTEHY.DatabaseCoursePortal.Api.Models.User;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class TeacherService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly FileService _fileService;
        private readonly UserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly MailService _mailService;
        private readonly TwilioService _twilioService;
        private readonly IMapper _mapper;

        public TeacherService(ApplicationDbContext dbContext, FileService fileService, IMapper mapper, UserService userService, UserManager<User> userManager, MailService mailService, TwilioService twilioService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _mapper = mapper;
            _userService = userService;
            _userManager = userManager;
            _mailService = mailService;
            _twilioService = twilioService;
        }

        public async Task<PagingResult<TeacherDto>> Get(GetTeacherRequest request)
        {
            var query = _dbContext.Teachers.Include(x => x.User).Where(x => x.DeletedAt == null).AsQueryable();

            if (!string.IsNullOrEmpty(request.NameOrEmail))
            {
                string search = request.NameOrEmail.ToLower();
                query = query.Where(b => b.User.Name.ToLower().Contains(request.NameOrEmail.ToLower()) || b.User.Email.ToLower().Contains(request.NameOrEmail.ToLower()));
            }

            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                query = query.Where(b => b.User.PhoneNumber.ToLower().Contains(request.PhoneNumber.ToLower()));
            }

            if (request.Status != null)
            {
                query = query.Where(b => b.User.Status == request.Status);
            }

            int total = await query.CountAsync();

            if (request.PageIndex == null) request.PageIndex = 1;
            if (request.PageSize == null) request.PageSize = total;

            int totalPages = (int)Math.Ceiling((double)total / request.PageSize.Value);

            if (string.IsNullOrEmpty(request.OrderBy) && string.IsNullOrEmpty(request.SortBy))
            {
                query = query.OrderByDescending(b => b.Id);
            }
            else if (string.IsNullOrEmpty(request.OrderBy))
            {
                if(request.SortBy == SortByConstant.Asc)
                {
                    query = query.OrderBy(b => b.Id);
                }
                else
                {
                    query = query.OrderByDescending(b => b.Id);
                }
            }
            else if (string.IsNullOrEmpty(request.SortBy))
            {
                query = query.OrderByDescending(b => b.Id);
            }
            else
            {
                if(request.OrderBy == OrderByConstant.Id && request.SortBy == SortByConstant.Asc)
                {
                    query = query.OrderBy(b => b.Id);
                }
                else if(request.OrderBy == OrderByConstant.Id && request.SortBy == SortByConstant.Desc)
                {
                    query = query.OrderByDescending(b => b.Id);
                }
            }

            var items = await query
            .Skip((request.PageIndex.Value - 1) * request.PageSize.Value)
            .Take(request.PageSize.Value)
            .ToListAsync();

            var itemsMapper = _mapper.Map<List<TeacherDto>>(items);

            var result = new PagingResult<TeacherDto>(itemsMapper, request.PageIndex.Value, request.PageSize.Value, total, totalPages);

            return result;
        }

        public async Task<Teacher> Create(CreateTeacherRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) && request.VerificationType == VerificationType.Email)
            {
                throw new ApiException("Dữ liệu trường email không được để trống khi chọn kiểu xác thực là email!", HttpStatusCode.BadRequest);
            }

            if (string.IsNullOrEmpty(request.Phone) && request.VerificationType == VerificationType.Phone)
            {
                throw new ApiException("Dữ liệu trường số điện thoại không được để trống khi chọn kiểu xác thực là số điện thoại!", HttpStatusCode.BadRequest);
            }

            var createUserRequest = _mapper.Map<CreateUserRequest>(request);
            createUserRequest.Role = Constants.Role.Teacher;

            var user = await _userService.Create(createUserRequest);

            var newTeacher = new Teacher()
            {
                UserId = user.Id,
                TeacherId = request.TeacherId,
            };

            await _dbContext.Teachers.AddAsync(newTeacher);
            await _dbContext.SaveChangesAsync();

            if(request.VerificationType == VerificationType.Email)
            {
                var otpCode = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);

                var mail = new SendMailRequest
                {
                    ToEmail = request.Email,
                    Subject = "Mã xác nhận tài khoản giáo viên UTEHY Database Course Portal",
                    Body = "Mã xác thực đăng nhập UTEHY DatabaseCourse của bạn là " + otpCode,
                };

                await _mailService.Send(mail);
            }
            else
            {
                var otpCode = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);

                string message = "Mã xác thực tài khoản giáo viên UTEHY Database Course của bạn là " + otpCode;
                await _twilioService.SendMessage(message, user.PhoneNumber);
            }

            return newTeacher;
        }

        public async Task<TeacherDto> GetById(int id)
        {
            var teacher = await _dbContext.Teachers.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);

            var result = _mapper.Map<TeacherDto>(teacher);

            return result;
        }
        public async Task<TeacherDto> GetByUserId(Guid userId)
        {
            var teacher = await _dbContext.Teachers.Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == userId);

            var result = _mapper.Map<TeacherDto>(teacher);

            return result;
        }

        public async Task<TeacherDto> Edit(EditTeacherRequest request)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var teacher = await _dbContext.Teachers.FindAsync(request.Id);

                    if (teacher == null)
                    {
                        throw new ApiException("Không tìm thấy giáo viên có Id hợp lệ!", HttpStatusCode.BadRequest);
                    }

                    teacher.TeacherId = request.TeacherId;

                    await _dbContext.SaveChangesAsync();

                    var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == teacher.UserId);

                    if(!string.IsNullOrEmpty(request.Phone) && request.Phone != user.PhoneNumber && user.PhoneNumberConfirmed)
                    {
                        throw new ApiException("Số điện thoại người dùng đã xác nhận, không thể thay đổi!", HttpStatusCode.BadRequest);
                    }

                    if (!string.IsNullOrEmpty(request.Email) && request.Email != user.Email && user.EmailConfirmed)
                    {
                        throw new ApiException("Email người dùng đã xác nhận, không thể thay đổi!", HttpStatusCode.BadRequest);
                    }

                    if (user == null)
                    {
                        throw new ApiException("Không tìm thấy người dùng liên kết với giáo viên!", HttpStatusCode.BadRequest);
                    }

                    user.Email = request.Email;
                    user.PhoneNumber = request.Phone;
                    user.Status = request.Status;
                    user.Name = request.Name;

                    await _dbContext.SaveChangesAsync();

                    transaction.Commit();

                    var result = _mapper.Map<TeacherDto>(teacher);

                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); 
                                            
                    throw new ApiException("Có lỗi xảy ra trong quá trình xử lý!", HttpStatusCode.InternalServerError, ex);
                }
            }

        }

        public async Task<TeacherDto> Delete(int id)
        {
            var teacher = await _dbContext.Teachers.FindAsync(id);

            if (teacher == null)
            {
                throw new ApiException("Không tìm thấy giáo viên có Id hợp lệ!", HttpStatusCode.BadRequest);
            }

            teacher.DeletedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == teacher.UserId);

            if (user == null)
            {
                throw new ApiException("Không tìm thấy giáo viên có Id hợp lệ!", HttpStatusCode.BadRequest);
            }

            user.DeletedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            var result = _mapper.Map<TeacherDto>(teacher);

            return result;
        }

        public async Task<DeleteMultipleResult<int>> DeleteMultiple(List<int> teacherIds)
        {
            var successfulIds = new List<int>();
            var failedIds = new List<int>();

            foreach (var teacherId in teacherIds)
            {
                var teacher = await _dbContext.Teachers.FindAsync(teacherId);

                if (teacher == null)
                {
                    failedIds.Add(teacherId);
                    continue;
                }

                teacher.DeletedAt = DateTime.Now;

                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == teacher.UserId);

                if (user != null)
                {
                    user.DeletedAt = DateTime.Now;
                }

                successfulIds.Add(teacherId);
            }

            await _dbContext.SaveChangesAsync();

            var result = new DeleteMultipleResult<int>
            {
                SuccessfulItems = successfulIds,
                FailedItems = failedIds
            };

            return result;
        }
    }
}
