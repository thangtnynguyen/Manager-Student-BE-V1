using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Enums;
using UTEHY.DatabaseCoursePortal.Api.Exceptions;
using UTEHY.DatabaseCoursePortal.Api.Models.Account;
using UTEHY.DatabaseCoursePortal.Api.Models.Auth;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Mail;
using UTEHY.DatabaseCoursePortal.Api.Models.User;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly UserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly TwilioService _twilioService;
        private readonly MailService _mailService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(ApplicationDbContext dbContext, IConfiguration config, UserService userService, UserManager<User> userManager,TwilioService twilioService, MailService mailService, IMapper mapper, SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _config = config;
            _userService = userService;
            _userManager = userManager;
            _twilioService = twilioService;
            _mailService = mailService;
            _mapper = mapper;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> CreateToken(User user)
        {
            var key = _config["Jwt:Key"];

            var permissions = await _userService.GetPermissionAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimType.UserName, user.UserName),
                new Claim(ClaimType.Email, user.Email),
            };

            foreach (var permission in permissions)
            {
                claims.Add(new Claim(ClaimType.Permission, permission));
            }

            var subject = new ClaimsIdentity(claims);
            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature);
            var expires = DateTime.UtcNow.AddDays(30);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }

        public async Task<bool> Logout1()
        {
            await _signInManager.SignOutAsync();

            return true;
        }

        public async Task<bool> SendOtpLoginPhone(SendOtpLoginPhoneRequest request)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.PhoneNumber == request.Phone && user.PhoneNumberConfirmed == true);

            //Create user
            if (user == null)
            {
                user = new User
                {
                    UserName = request.Phone,
                    PhoneNumber = request.Phone,
                    PhoneNumberConfirmed = true,
                    Name = request.Name,
                };

                await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, "Student");
            }

            //Verify login
            var otpCode = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);

            //Send otp
            string message = "Mã xác thực đăng nhập UTEHY DatabaseCourse của bạn là " + otpCode;
            await _twilioService.SendMessage(message, user.PhoneNumber);

            return true;
        }

        public async Task<string> VerifyOtpLoginPhone(VerifyOtpLoginPhoneRequest request)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.PhoneNumber == request.Phone && user.PhoneNumberConfirmed == true);

            //Create user
            if (user == null)
            {
                throw new ApiException("Số điện thoại không tồn tại trong hệ thống!",HttpStatusCode.BadRequest);
            }
            else
            {
                if (request.Name != null)
                {
                    user.Name = request.Name;
                }
                await _userManager.UpdateAsync(user);
            }

            var isOtpValid = await _userManager.VerifyChangePhoneNumberTokenAsync(user, request.Otp, user.PhoneNumber);

            if (!isOtpValid)
            {
                throw new ApiException("Mã OTP không hợp lệ!",HttpStatusCode.BadRequest);
            }

            //Create token
            var token = await CreateToken(user);

            if (token == null)
            {
                throw new ApiException("Tạo mã token thất bại!", HttpStatusCode.BadRequest);
            }

            return token;
        }

        public async Task<bool> SendOtpLoginEmail(SendOtpLoginEmailRequest request)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.Email == request.Email && user.EmailConfirmed == true);

            //Create user
            if (user == null)
            {
                user = new User
                {
                    UserName = request.Email,
                    Email = request.Email,
                    EmailConfirmed = true,
                    Name = request.Name,
                };

                var res = await _userManager.CreateAsync(user);
                await _userManager.AddToRoleAsync(user, Constants.Role.Student);
            }

            //Verify logins
            var otpCode = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);

            //Send otp
            var mail = new SendMailRequest
            {
                ToEmail = request.Email,
                Subject = "Mã xác nhận đăng nhập UTEHY DatabaseCourse",
                Body = "Mã xác thực đăng nhập UTEHY DatabaseCourse của bạn là " + otpCode,
            };
            await _mailService.Send(mail);

            return true;
        }

        public async Task<string> VerifyOtpLoginEmail(VerifyOtpLoginEmailRequest request)
        {
            var user = _dbContext.Users.FirstOrDefault(user => user.Email == request.Email && user.EmailConfirmed == true);

            //Get user
            if (user == null)
            {
                throw new ApiException("Email không tồn tại trong hệ thống!", HttpStatusCode.BadRequest);
            }
            else
            {
                if (request.Name != null)
                {
                    user.Name = request.Name;
                }
                await _userManager.UpdateAsync(user);

                if(request.Password != null)
                {
                    var tokenReset = await _userManager.GeneratePasswordResetTokenAsync(user);

                    await _userManager.ResetPasswordAsync(user, tokenReset, request.Password);
                }
            }

            var isOtpValid = await _userManager.VerifyChangePhoneNumberTokenAsync(user, request.Otp, user.PhoneNumber);

            if (!isOtpValid)
            {
                throw new ApiException("Mã OTP không hợp lệ!", HttpStatusCode.BadRequest);
            }

            //Create token
            var token = await CreateToken(user);

            if (token == null)
            {
                throw new ApiException("Tạo mã token thất bại!", HttpStatusCode.BadRequest);
            }

            return token;
        }

        #region Token
        public JwtSecurityToken CreateToken(List<Claim> claims)
        {
            var key = _config["Jwt:Key"];

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key), "Không thể tải cấu hình Key Jwt!");
            }

            var minuteValidToken = _config["Jwt:TokenValidityInMinutes"];

            if (string.IsNullOrEmpty(minuteValidToken))
            {
                throw new ArgumentNullException(nameof(minuteValidToken), "Không thể tải cấu hình TokenValidityInMinutes Jwt!");
            }

            var issuer = _config["Jwt:Issuer"];

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key), "Không thể tải cấu hình Issuer Jwt!");
            }

            var audience = _config["Jwt:Audience"];

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key), "Không thể tải cấu hình Audience Jwt!");
            }

            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddMinutes(int.Parse(minuteValidToken));

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: expires,
                claims: claims,
                signingCredentials: creds
             );

            return token;
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<bool> RevokeRefreshToken(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                throw new BadHttpRequestException("Tên người dùng không tồn tại trong hệ thống!");
            }

            user.RefreshToken = null;

            await _userManager.UpdateAsync(user);

            return true;
        }

        public async Task<bool> RevokeAllRefreshToken()
        {
            var users = _userManager.Users.ToList();

            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            return true;
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var key = _config["Jwt:Key"];

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key), "Không thể tải cấu hình Key Jwt!");
            }

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Token không hợp lệ!");
            }

            return principal;

        }

        public async Task<LoginResult> RefreshToken(string refreshToken)
        {
            var authorizationHeader = _httpContextAccessor?.HttpContext?.Request.Headers[HeaderRequest.Authorization].ToString();

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                throw new BadHttpRequestException("AccessToken không tồn tại trong yêu cầu!");
            }

            var token = authorizationHeader.Split(' ').LastOrDefault();

            if (string.IsNullOrEmpty(token))
            {
                throw new BadHttpRequestException("AccessToken không hợp lệ!");
            }

            var principal = GetPrincipalFromExpiredToken(token);

            string username = principal.Claims.FirstOrDefault(x => x.Type == ClaimType.UserName).Value;

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                throw new BadHttpRequestException("Token chứa thông tin người dùng không tồn tại trong hệ thống!");
            }

            if (user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now){
                throw new BadHttpRequestException("RefreshToken không hợp lệ hoặc đã hết hạn!");
            }

            var newAccessToken = this.CreateToken(principal.Claims.ToList());
            var newRefreshToken = this.CreateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(newAccessToken);

            var loginResult = new LoginResult()
            {
                AccessToken = tokenString,
                RefreshToken = newRefreshToken,
                Expiration = newAccessToken.ValidTo
            };

            return loginResult;
        }
        #endregion

        #region User
        public async Task<LoginResult> LoginByUsername(LoginUsernameRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
            {
                throw new BadHttpRequestException("Tên người dùng không tồn tại trong hệ thống!");
            }

            UserType userType = (UserType)request.Type;
            string typeRole = Enum.GetName(typeof(UserType), userType).ToLower().Trim();

            var roles = await _userManager.GetRolesAsync(user);

            if (!roles.Contains(typeRole))
            {
                throw new UnauthorizedAccessException($"Người dùng không có vai trò {typeRole}!");
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);

            if (!result.Succeeded)
            {
                throw new BadHttpRequestException("Tên tài khoản hoặc mật khẩu không chính xác!");
            }

            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimType.UserName, user.UserName),
                new Claim(ClaimType.Email, user.Email),
            };

            if (userClaims != null)
            {
                claims.AddRange(userClaims);
            }

            //Create token
            var token = this.CreateToken(claims);
            var refreshToken = this.CreateRefreshToken();

            var refreshTokenValidityInDays = _config["Jwt:RefreshTokenValidityInDays"];

            if (string.IsNullOrEmpty(refreshTokenValidityInDays))
            {
                throw new ArgumentNullException(nameof(refreshTokenValidityInDays), "Không thể tải cấu hình RefreshTokenValidityInDays Jwt!");
            }

            var refreshTokenExpiryTime = DateTime.Now.AddDays(int.Parse(refreshTokenValidityInDays));

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = refreshTokenExpiryTime;

            await _userManager.UpdateAsync(user);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            var loginResult = new LoginResult()
            {
                AccessToken = tokenString,
                RefreshToken = refreshToken,
                Expiration = token.ValidTo
            };

            return loginResult;
        }

        public async Task<UserDto> GetUserInfo()
        {
            var user = await _userService.GetUserCurrentAsync();

            if(user == null)
            {
                throw new Exception("Người dùng không tồn tại!");
            }

            var permissions = await _userService.GetPermissionAsync(user);

            var userDto = _mapper.Map<UserDto>(user);

            //lấy các lớp mà sinh viên đã tham gia
            if (permissions.Contains(Constants.Permission.Student))
            {

                var studentId = await _dbContext.Students
                                   .Where(s => s.UserId == user.Id)
                                   .Select(s => s.Id)
                                   .SingleOrDefaultAsync();

                //if (studentId == default)
                //{
                //    throw new ApiException("Không tìm thấy học sinh hợp lệ!", Constants.HttpStatusCode.BadRequest);
                //}
                if (studentId != null)
                {
                    List<int> groupModuleIds = await _dbContext.StudentGroupModules
                                                   .Where(sgm => sgm.StudentId == studentId)
                                                   .Select(sgm => sgm.GroupModuleId).Cast<int>()
                                                   .ToListAsync();

                    userDto.GroupModuleIds = groupModuleIds;
                }

              

            }

          

            userDto.Permissions = permissions;

            return userDto;
        }

        public async Task<bool> Logout()
        {
            await _signInManager.SignOutAsync();

            User user = await _userService.GetUserCurrentAsync();

            await this.RevokeRefreshToken(user.UserName);

            return true;
        }
        #endregion
    }
}
