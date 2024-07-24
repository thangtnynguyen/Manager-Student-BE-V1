using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Track;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class TrackMapper : Profile
    {
        public TrackMapper()
        {
            CreateMap<Track, CreateTrackRequest>();
            CreateMap<CreateTrackRequest, Track>();
            CreateMap<StepTrack, CreateStepTrackRequest>();
            CreateMap<CreateStepTrackRequest, StepTrack>();
            CreateMap<CreateAssignmentCourseRequest, AssignmentCourse>();
            CreateMap<AssignmentCourse, CreateAssignmentCourseRequest>();
            CreateMap<QuizCourseRequest, QuizCourse>();
            CreateMap<QuizCourse, QuizCourseRequest>();
        }
    }
}
