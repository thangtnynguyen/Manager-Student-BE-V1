namespace UTEHY.DatabaseCoursePortal.Api.Models.User
{
    public class EditUserInfoRequest
    {
        public Guid Id { get; set; }    

        public string? PhoneNumber { get; set; }

        public string? Name { get; set; }
    }
}
