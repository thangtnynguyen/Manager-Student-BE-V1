using AutoMapper;
using UTEHY.DatabaseCoursePortal.Api.Configs;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;

using UTEHY.DatabaseCoursePortal.Api.Models.Exam;
using UTEHY.DatabaseCoursePortal.Api.Models.ExamResult;

namespace UTEHY.DatabaseCoursePortal.Api.Mappers
{
    public class ExamResultMapper : Profile
    {
        public ExamResultMapper()
        {
            CreateMap<ExamResult, GetExamResultRequest>();
            CreateMap<GetExamResultRequest, ExamResult>();
            CreateMap<ExamResult, ExamResultDto>();
             //.ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.User.Name));
            CreateMap<ExamResultDto, ExamResult>();

            CreateMap<CreateExamResultRequest, ExamResult>();
            CreateMap<ExamResult, CreateExamResultRequest>();




        }
    }
}
