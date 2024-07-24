using UTEHY.DatabaseCoursePortal.Api.Constants;

namespace UTEHY.DatabaseCoursePortal.Api.Exceptions
{
    public class ApiException : Exception
    {
        public int Status { get; }
        public object Data { get; }

        public ApiException(string message, int status = HttpStatusCode.BadRequest, object data = null) : base(message)
        {
            Status = status;
            Data = data;
        }
    }
}