using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Security.Claims;
using System.Xml.Linq;
using Twilio.Rest.Api.V2010.Account;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities.Interface;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Exceptions;
using UTEHY.DatabaseCoursePortal.Api.Models.Account;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Post;
using UTEHY.DatabaseCoursePortal.Api.Models.User;
using static System.Net.WebRequestMethods;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly FileService _fileService;
        private readonly ConfigService _configService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext dbContext, UserManager<User> userManager, FileService fileService, ConfigService configService, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _fileService = fileService;
            _configService = configService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        #region Auth
        public async Task<User> GetUserCurrentAsync()
        {
            var username = _httpContextAccessor?.HttpContext?.User.FindFirst(x => x.Type == ClaimType.UserName)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                throw new UnauthorizedAccessException("Người dùng chưa đăng nhập hoặc phiên làm việc đã hết hạn.");
            }

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                throw new BadHttpRequestException("Người dùng không tồn tại trong hệ thống!");
            }

            return user;
        }
        #endregion

        #region Action
        public async Task AttachCreateInfo<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            var userCurrent = await GetUserCurrentAsync();
            entity.CreatedAt = DateTime.Now;
            entity.CreatedBy = userCurrent?.Id;
        }

        public async Task AttachUpdateInfo<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            var userCurrent = await GetUserCurrentAsync();
            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = userCurrent?.Id;
        }

        public async Task AttachDeleteInfo<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            var userCurrent = await GetUserCurrentAsync();
            entity.DeletedAt = DateTime.Now;
            entity.DeletedBy = userCurrent?.Id;
        }
        #endregion


        public async Task<User> Create(CreateUserRequest request)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(user =>
                (user.PhoneNumber == request.Phone && user.PhoneNumberConfirmed && !string.IsNullOrEmpty(request.Phone)) ||
                (user.Email == request.Email && user.EmailConfirmed && !string.IsNullOrEmpty(request.Email)));

            var userCreationCountConfig = await _configService.GetConfigValue(ConfigConstant.UserCreationCount);
            var userCreationCount = int.Parse(userCreationCountConfig);
            var username = Prefix.Username + (userCreationCount + 1);

            if (existingUser != null)
            {
                string duplicateField = existingUser.PhoneNumber == request.Phone ? "Số điện thoại" : "Email";
                throw new ApiException($"{duplicateField} đã tồn tại trong hệ thống!", HttpStatusCode.BadRequest);
            }

            var existingUsernameUser = await _userManager.FindByNameAsync(username);
            while (existingUsernameUser != null)
            {
                userCreationCount++;
                username = Prefix.Username + (userCreationCount + 1);
                existingUsernameUser = await _userManager.FindByNameAsync(username);
            }

            if (request.AvatarFile?.Length > 0)
            {
                request.AvatarUrl = await _fileService.UploadFileAsync(request.AvatarFile, PathFolder.User);
            }

            var newUser = new User()
            {
                UserName = username,
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.Phone,
                AvatarUrl = string.IsNullOrEmpty(request.AvatarUrl) ? ResourceConstant.DefaultAvatarUrl : request.AvatarUrl,
                Status = request.Status,
            };

            if (string.IsNullOrEmpty(request.Password))
            {
                var result = await _userManager.CreateAsync(newUser);

                if (!result.Succeeded)
                {
                    throw new ApiException($"Không thể tạo người dùng. Lỗi: {string.Join(", ", result.Errors)}", HttpStatusCode.BadRequest);
                }
            }
            else
            {
                var result = await _userManager.CreateAsync(newUser, request.Password);

                if (!result.Succeeded)
                {
                    throw new ApiException($"Không thể tạo người dùng. Lỗi: {string.Join(", ", result.Errors)}", HttpStatusCode.BadRequest);
                }
            }

            await _userManager.AddToRoleAsync(newUser, request.Role);

            await _configService.UpdateConfigValue(ConfigConstant.UserCreationCount, (userCreationCount + 1).ToString());

            return newUser;
        }

        public async Task<UserDto> GetUserInfo(HttpContext httpContext)
        {
            try
            {
                var email = httpContext.User.Claims.First(x => x.Type == "Email").Value;

                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

                if (user == null)
                {
                    throw new ApiException("Không tìm thấy người dùng hợp lệ!", Constants.HttpStatusCode.BadRequest);
                }

                var permissions = await GetPermissionAsync(user);

                var userDto = _mapper.Map<UserDto>(user);


                userDto.Permissions = permissions;


                return userDto;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<User?> GetCurrentUserAsync()
        {
            return await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
        }

        public async Task<List<User>> Test()
        {
            var test = await _dbContext.Users.ToListAsync();


            return test;
        }

        public async Task<User> EditUserInfo(EditUserInfoRequest request)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var user = await _dbContext.Users.FindAsync(request.Id);

                    if (user == null)
                    {
                        throw new ApiException("Không tìm thấy user có Id hợp lệ!", HttpStatusCode.BadRequest);
                    }

                    user.Name = request.Name;

                    user.PhoneNumber = request.PhoneNumber;

                    await _dbContext.SaveChangesAsync();

                    transaction.Commit();

                    return user;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    throw new ApiException("Có lỗi xảy ra trong quá trình xử lý!", HttpStatusCode.InternalServerError, ex);
                }
            }

        }

        

        public async Task<List<string>> GetPermissionAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var permissions = await _dbContext.Roles.Where(role => roles.Contains(role.Name))
            .SelectMany(role => role.RolePermissions)
            .Select(rolePermission => rolePermission.Permission.Name).ToListAsync();

            return permissions;
        }

        public async Task<List<string>> GetUserWithRolePermission(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var permissions = await _dbContext.Roles.Where(role => roles.Contains(role.Name))
            .SelectMany(role => role.RolePermissions)
            .Select(rolePermission => rolePermission.Permission.Name).ToListAsync();

            return permissions;
        }

        

        //public async Task<string> GenerateAutoUsername()
        //{
        //    //var userCount = await _dbContext.Users.fi;

        //    return $"User{userCount + 1}";
        //}

        public async Task<Student> GetStudentByUser(Guid userId)
        {

            var student = await _dbContext.Students
                                 .FirstOrDefaultAsync(s => s.UserId == userId);

            return student;
        }


    }
}
