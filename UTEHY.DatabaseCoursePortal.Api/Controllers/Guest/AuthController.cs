using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Services;
using UTEHY.DatabaseCoursePortal.Api.Models.Account;
using UTEHY.DatabaseCoursePortal.Api.Models.Auth;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.User;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers.Guest
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;                           
        }

        [HttpGet("user-current")]
        public async Task<ApiResult<UserDto>> GetUserInfo()
        {
            try
            {
                var user = await _authService.GetUserInfo();

                return new ApiResult<UserDto>()
                {
                    Status = true,
                    Message = "Lấy thông tin người dùng thành công!",
                    Data = user
                };
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ApiResult<LoginResult>> LoginByUsername([FromBody] LoginUsernameRequest request)
        {
            try
            {
                var loginResult = await _authService.LoginByUsername(request);

                return new ApiResult<LoginResult>()
                {
                    Status = true,
                    Message = "Đăng nhập thành công!",
                    Data = loginResult
                };
            }
            catch (BadHttpRequestException ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }
            catch(UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("refresh-token")]
        public async Task<ApiResult<LoginResult>> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var loginResult = await _authService.RefreshToken(request.RefreshToken);

                return new ApiResult<LoginResult>()
                {
                    Status = true,
                    Message = "Đăng nhập thành công!",
                    Data = loginResult
                };
            }
            catch (BadHttpRequestException ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<ApiResult<bool>> Logout()
        {
            try
            {
                var isLogout = await _authService.Logout();

                return new ApiResult<bool>()
                {
                    Status = true,
                    Message = "Đăng xuất thành công!",
                    Data = isLogout
                };
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException(ex.Message);
            }
            catch (BadHttpRequestException ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
