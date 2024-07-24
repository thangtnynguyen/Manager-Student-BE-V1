namespace UTEHY.DatabaseCoursePortal.Api.Models.Common
{
    public class ApiResult<T>
    {
        public bool Status { get; set; }

        public string? Message { get; set; }

        public T? Data { get; set; }
    }
}
