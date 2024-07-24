namespace UTEHY.DatabaseCoursePortal.Api.Models.Schedule
{
    public class CreateListScheduleRequest
    {
        public int GroupModuleId { get; set; }
        public List<CreateScheduleRequest> Schedules { get; set; }
    }
}
