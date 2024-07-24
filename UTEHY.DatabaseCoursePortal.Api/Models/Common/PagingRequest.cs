namespace UTEHY.DatabaseCoursePortal.Api.Models.Common
{
    public class PagingRequest
    {
        public int? PageIndex { get; set; }

        public int? PageSize { get; set; }

        public string? SortBy { get; set; }

        public string? OrderBy { get; set; }
    }
}
