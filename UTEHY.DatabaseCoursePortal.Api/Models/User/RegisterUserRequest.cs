using UTEHY.DatabaseCoursePortal.Api.Enums;
using Newtonsoft.Json;


namespace UTEHY.DatabaseCoursePortal.Api.Models.User
{
    public class RegisterUserRequest
    {
        public string? Name { get; set; }

        public IFormFile? AvatarFile { get; set; }

        [JsonIgnore]
        public string? AvatarUrl { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? Password { get; set; }

        public bool? Status { get; set; }

        public string? Role { get; set; }

        /// <summary>
        /// 1.Điện thoại 2.Email
        /// </summary>
        public VerificationType VerificationType { get; set; }
    }
}
