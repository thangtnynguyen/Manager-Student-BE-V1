namespace UTEHY.DatabaseCoursePortal.Api.Models.Attendence
{
    public class CreateAttendenceRequest
    {
        public string? StudentId { get; set; }
        public int ScheduleId { get; set; }
        public string? Note{ get; set; }
        public bool Attendant { get; set; }
        public bool PermittedLeave { get; set; }
        public bool UnpermittedLeave { get; set; }
    }
}
