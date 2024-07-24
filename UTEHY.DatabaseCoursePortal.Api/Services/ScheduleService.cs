using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Schedule;
using UTEHY.DatabaseCoursePortal.Api.Exceptions;
using MoreLinq;
using UTEHY.DatabaseCoursePortal.Api.Models.Attendence;
using OfficeOpenXml;
using Twilio.Rest.Api.V2010.Account;
using UTEHY.DatabaseCoursePortal.Api.Models.Student;
using UTEHY.DatabaseCoursePortal.Api.Models.GroupModule;
using UTEHY.DatabaseCoursePortal.Api.Helpers;
using OfficeOpenXml.Style;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class ScheduleService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly AttendenceService _attendenceService;
        private readonly GroupModuleService _groupModuleService;

        public ScheduleService(IMapper mapper, ApplicationDbContext dbContext, AttendenceService attendenceService, GroupModuleService groupModuleService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _attendenceService = attendenceService;
            _groupModuleService = groupModuleService;
        }

        public async Task<PagingResult<Schedule>> Get(GetScheduleRequest request)
        {
            try
            {
                var query = _dbContext.Schedules.Where(x => x.DeletedAt == null).AsQueryable();

                if(request.GroupModuleId != null)
                {
                    query = query.Where(x => x.GroupModuleId == request.GroupModuleId);
                }

                int total = await query.CountAsync();

                if (request.PageIndex == null) request.PageIndex = 1;
                if (request.PageSize == null) request.PageSize = total;

                int totalPages = (int)Math.Ceiling((double)total / request.PageSize.Value);

                if (string.IsNullOrEmpty(request.OrderBy) && string.IsNullOrEmpty(request.SortBy))
                {
                    query = query.OrderByDescending(b => b.Id);
                }
                else if (string.IsNullOrEmpty(request.OrderBy))
                {
                    if (request.SortBy == SortByConstant.Asc)
                    {
                        query = query.OrderBy(b => b.Id);
                    }
                    else
                    {
                        query = query.OrderByDescending(b => b.Id);
                    }
                }
                else if (string.IsNullOrEmpty(request.SortBy))
                {
                    query = query.OrderByDescending(b => b.Id);
                }
                else
                {
                    if (request.OrderBy == OrderByConstant.Id && request.SortBy == SortByConstant.Asc)
                    {
                        query = query.OrderBy(b => b.Id);
                    }
                    else if (request.OrderBy == OrderByConstant.Id && request.SortBy == SortByConstant.Desc)
                    {
                        query = query.OrderByDescending(b => b.Id);
                    }
                }

                var items = await query
                    .Skip((request.PageIndex.Value - 1) * request.PageSize.Value)
                    .Take(request.PageSize.Value)
                    .ToListAsync();

                for(int i = 0; i < items.Count; i++)
                {
                    var attendenceRequest = new GetAttendenceRequest
                    {
                        ScheduleId = items[i].Id
                    };
                    var attendences = await _attendenceService.Get(attendenceRequest);
                    items[i].Attendances = attendences.Items;
                }

                var result = new PagingResult<Schedule>(items, request.PageIndex.Value, request.PageSize.Value, request.SortBy, request.OrderBy, total, totalPages);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<Schedule> Create(CreateScheduleRequest request)
        {
            try
            {
                var groupModule = await _dbContext.GroupModules.FindAsync(request.GroupModuleId);
                if (groupModule == null)
                {
                    throw new ApiException("Mã lớp không tồn tại trong hệ thống!", HttpStatusCode.InternalServerError);
                }

                // create schedule
                var scheduleCreate = new Schedule
                {
                    GroupModuleId = request.GroupModuleId,
                    ClassPeriods = request.ClassPeriods,
                    DateSchool = DateTime.Parse(request.DateSchool),
                    ClassRoom = request.ClassRoom
                };

                await _dbContext.Schedules.AddAsync(scheduleCreate);
                await _dbContext.SaveChangesAsync();

                return scheduleCreate;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }
        public async Task<bool> CreateListSchedule(List<CreateScheduleRequest> request)
        {
            try
            {
                for(int i = 0; i< request.Count; i++)
                {
                    var groupModule = await _dbContext.GroupModules.FindAsync(request[i].GroupModuleId);
                    if (groupModule == null)
                    {
                        throw new ApiException("Mã lớp không tồn tại trong hệ thống!", HttpStatusCode.InternalServerError);
                    }

                    // create schedule
                    var scheduleCreate = new Schedule
                    {
                        GroupModuleId = request[i].GroupModuleId,
                        ClassPeriods = request[i].ClassPeriods,
                        DateSchool = DateTime.Parse(request[i].DateSchool),
                        ClassRoom = request[i].ClassRoom
                    };

                    await _dbContext.Schedules.AddAsync(scheduleCreate);
                }
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<byte[]> ExportAttendenceSheet(GetScheduleRequest request)
        {
            var scheduleResult = await Get(request);
            if(scheduleResult.Items != null && scheduleResult.Items.Count != 0)
            {
                try
                {
                    var schedules = scheduleResult.Items;

                    var requestStudents = new GetStudentsGroupModuleRequest()
                    {
                        GroupModuleId = request.GroupModuleId
                    };
                    var studentsResult = await _groupModuleService.GetStudentsGroupModule(requestStudents);
                    var students = studentsResult.Items;

                    using (var package = new ExcelPackage())
                    {
                        var worksheet = package.Workbook.Worksheets.Add(ExportFile.ListScoreStudentExcelTab);
                        ExcelRange mergedCells = worksheet.Cells["A1:D1"];
                        mergedCells.Merge = true;

                        worksheet.Cells[1, 1].Value = "C: có mặt, P: Nghỉ có phép, K: Nghỉ không phép";
                        worksheet.Cells[2, 1].Value = "STT";
                        worksheet.Cells[2, 2].Value = "Họ và tên";
                        worksheet.Cells[2, 3].Value = "Mã SV";

                        // tổng số buổi có mặt
                        List<double> numberLessonPresent = new List<double>();
                        // tổng số buổi nghỉ
                        List<double> numberLessonAbsented = new List<double>();

                        List<string> listStudentId = new List<string>();

                        if(students != null && students.Count > 0) 
                        {
                            for (int i = 0; i < students.Count; i++)
                            {
                                worksheet.Cells[i + 3, 1].Value = i + 1;
                                worksheet.Cells[i + 3, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                worksheet.Cells[i + 3, 2].Value = students[i].User.Name;
                                worksheet.Cells[i + 3, 3].Value = students[i].StudentId;
                                worksheet.Cells[i + 3, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                worksheet.Cells[i + 3, 4 + schedules.Count].Value = schedules.Count;
                                worksheet.Cells[i + 3, 4 + schedules.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                                // khởi tạo giá trị ban đầu cho danh sách đếm, giá trị ban đầu = 0;
                                // mỗi phần tử tương ứng với 1 sinh viên, giá trị ban đầu = 0;
                                numberLessonPresent.Add(0);
                                numberLessonAbsented.Add(0);

                                // lưu trữ list studentId để hiển thị bảng điểm danh lần lượt theo listStudentId
                                listStudentId.Add(students[i].StudentId);
                            }
                        }
                        int numberLessonStudied = 0;
                        for (int i = 0; i < schedules.Count; i++)
                        {

                            // sắp xếp lại bảng điểm danh lần lượt theo listStudentId
                            List<Attendance> sortedAttendences = listStudentId
                                .Join(schedules[i].Attendances, id => id, attendance => attendance.StudentId, (id, attendance) => attendance)
                                .ToList();

                            worksheet.Cells[2, i + 4].Value = $"{schedules[i].DateSchool.Day}/{schedules[i].DateSchool.Month}/{schedules[i].DateSchool.Year}";

                            for (int j = 0; j < sortedAttendences.Count; j++)
                            {
                                numberLessonStudied++;
                                string attendenceValue = "";
                                if (sortedAttendences[j].Attendant == true)
                                {
                                    numberLessonPresent[j] = numberLessonPresent[j] + 1;
                                    attendenceValue = "C";
                                }    
                                else if (sortedAttendences[j].PermittedLeave == true)
                                {
                                    numberLessonAbsented[j] = numberLessonAbsented[j] + 1;
                                    attendenceValue = "P";
                                }    
                                else if (sortedAttendences[j].UnpermittedLeave == true)
                                {
                                    numberLessonAbsented[j] = numberLessonAbsented[j] + 1;
                                    attendenceValue = "K";
                                }    

                                // j: row, i: col
                                worksheet.Cells[j + 3, i + 4].Value = attendenceValue;
                                worksheet.Cells[j + 3, i + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            }
                        }
                        for (int i = 0; i < students.Count; i++)
                        {
                            worksheet.Cells[i + 3, 5 + schedules.Count].Value = numberLessonStudied / students.Count;
                            worksheet.Cells[i + 3, 5 + schedules.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }
                        worksheet.Cells[2, 4 + schedules.Count].Value = "Tổng số buổi";
                        worksheet.Cells[2, 5 + schedules.Count].Value = "Tổng số buổi đã học";
                        worksheet.Cells[2, 6 + schedules.Count].Value = "Có mặt";
                        worksheet.Cells[2, 7 + schedules.Count].Value = "Vắng";
                        worksheet.Cells[2, 8 + schedules.Count].Value = "Tỉ lệ";

                        for(int i = 0; i < numberLessonPresent.Count; i++)
                        {
                            worksheet.Cells[i + 3, 6 + schedules.Count].Value = numberLessonPresent[i];
                            worksheet.Cells[i + 3, 6 + schedules.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            worksheet.Cells[i + 3, 7 + schedules.Count].Value = numberLessonAbsented[i];
                            worksheet.Cells[i + 3, 7 + schedules.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            worksheet.Cells[i + 3, 8 + schedules.Count].Value = numberLessonPresent[i] != 0 ? ((numberLessonPresent[i]/ (numberLessonStudied / students.Count)) * 100) + "%" : 0;
                            worksheet.Cells[i + 3, 8 + schedules.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }

                        ExcelRange headerRow = worksheet.Cells["A2:" + StringHelper.NumberToLetter(8 + schedules.Count) + "2"];
                        headerRow.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        headerRow.Style.Font.Bold = true;

                        worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                        using (var stream = new MemoryStream())
                        {
                            package.SaveAs(stream);

                            stream.Position = 0;

                            return stream.ToArray();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
                }
            }
            else
            {
                throw new ApiException("Không có bảng điểm danh");
            }
        }
    }
}
