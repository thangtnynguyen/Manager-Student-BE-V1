namespace UTEHY.DatabaseCoursePortal.Api.Models.Common
{
    public class DeleteMultipleResult<T>
    {
        public List<T>? SuccessfulItems { set; get; }

        public List<T>? FailedItems  { set; get; }
    }
}
