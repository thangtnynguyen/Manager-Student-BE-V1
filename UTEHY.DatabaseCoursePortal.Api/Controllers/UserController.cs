using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Question;
using UTEHY.DatabaseCoursePortal.Api.Models.User;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly UserService _userService;

        public UserController(UserManager<User> userManager, UserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        [HttpGet("check-exists")]
        public async Task<ApiResult<string>> CheckUserExists([FromQuery] string field, string value)
        {
            User user = new User();

            switch (field.ToLower())
            {
                case "email":
                    user = await _userManager.FindByEmailAsync(value);
                    break;
                default:
                    return new ApiResult<string>()
                    {
                        Status = false,
                        Message = "Tên trường tìm kiếm không hợp lệ!",
                    };
            }

            if (user == null)
            {
                return new ApiResult<string>()
                {
                    Status = true,
                    Message = "Người dùng không tồn tại!",
                    Data = null
                };
            }

            return new ApiResult<string>()
            {
                Status = true,
                Message = "Người dùng tồn tại!",
                Data = null
            };
        }

        [HttpGet("user-info")]
        [HttpGet]
        public async Task<ApiResult<UserDto>> GetCurrentUser()
        {
            var user = await _userService.GetUserInfo(HttpContext);

            if (user == null)
            {
                return new ApiResult<UserDto>()
                {
                    Status = false,
                    Message = "Không tìm thấy thông tin người dùng hợp lệ!",
                    Data = null
                };
            }

            return new ApiResult<UserDto>()
            {
                Status = true,
                Message = "Lấy thông tin người dùng thành công!",
                Data = user
            };
        }

        [HttpGet("test")]
        public async Task<ApiResult<List<User>>> Test()
        {
            var user = await _userService.Test();

           

            return new ApiResult<List<User>>()
            {
                Status = true,
                Message = "Lấy thông tin người dùng thành công!",
                Data = user
            };
        }

        [HttpPost("edit-user-info")]
        public async Task<ApiResult<User>> EditUserInfo([FromBody] EditUserInfoRequest request)
        {
            var result = await _userService.EditUserInfo(request);

            return new ApiResult<User>()
            {
                Status = true,
                Message = "Cập nhật thông tin người dùng thành công!",
                Data = result
            };
        }
    }
}
