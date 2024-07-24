using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Exceptions;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.GroupModule;
using System.Drawing;
using UTEHY.DatabaseCoursePortal.Api.Helpers;
using System.Globalization;
using UTEHY.DatabaseCoursePortal.Api.Models.Student;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class GroupModuleService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly ConfigService _configService;
        private readonly IMapper _mapper;

        public GroupModuleService(ApplicationDbContext dbContext, IMapper mapper, UserService userService, UserManager<User> userManager, ConfigService configService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userService = userService;
            _userManager = userManager;
            _configService = configService;
        }

        public async Task<PagingResult<Data.Entities.GroupModule>> Get(GetGroupModuleRequest request)
        {
            try
            {
                var query = _dbContext.GroupModules.Where(x => x.DeletedAt == null)
                    .Include(x => x.StudentGroupModules)
                    .Include(x => x.Teacher).ThenInclude(t => t.User).Where(t => t.DeletedAt == null)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(b => b.Name.ToLower().Contains(request.Name.ToLower()));
                }

                if (request.Status != null)
                {
                    query = query.Where(b => b.Status == request.Status);
                }

                if (request.Year != null)
                {
                    query = query.Where(b => b.Year == request.Year);
                }

                if (request.Semester != null)
                {
                    query = query.Where(b => b.Semester == request.Semester);
                }

                if (request.TeacherId != null)
                {
                    query = query.Where(b => b.TeacherId == request.TeacherId);
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

                var itemsMapper = _mapper.Map<List<GroupModuleDto>>(items);


                var result = new PagingResult<Data.Entities.GroupModule>(items, request.PageIndex.Value, request.PageSize.Value, request.SortBy, request.OrderBy, total, totalPages);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<PagingResult<Student>> GetStudentsGroupModule(GetStudentsGroupModuleRequest request)
        {
            try
            {
                var query = _dbContext.GroupModules
                .Where(x => x.DeletedAt == null && x.Id == request.GroupModuleId)
                .Include(x => x.StudentGroupModules)
                .ThenInclude(xs => xs.Student)
                .ThenInclude(s => s.User)
                .SelectMany(x => x.StudentGroupModules.Select(sm => sm.Student))
                .Where(s => s.DeletedAt == null)
                .AsQueryable();

                if (!string.IsNullOrEmpty(request.NameOrEmail))
                {
                    string search = request.NameOrEmail.ToLower();
                    query = query.Where(b => b.User.Name.ToLower().Contains(request.NameOrEmail.ToLower()) || b.User.Email.ToLower().Contains(request.NameOrEmail.ToLower()));
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

                var result = new PagingResult<Student>(items, request.PageIndex.Value, request.PageSize.Value, request.SortBy, request.OrderBy, total, totalPages);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<Data.Entities.GroupModule> GetById(int id)
        {
            try
            {
                var banner = await _dbContext.GroupModules.Include(x => x.StudentGroupModules).FirstOrDefaultAsync(x => x.Id == id && x.DeletedAt == null);

                if (banner == null)
                {
                    throw new ApiException("Không tìm thấy nhóm học phần hợp lệ!", HttpStatusCode.InternalServerError);
                }

                return banner;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }
        public async Task<List<Data.Entities.GroupModule>> GetByExamId(int id)
        {
            try
            {
                var groupModule = await _dbContext.GroupModules
                .Include(x => x.ExamGroupModules)
                 .Where(group => group.DeletedAt == null &&
                    group.ExamGroupModules.Any(examGroupModule => examGroupModule.ExamId == id))
                 .ToListAsync();
                if (groupModule == null)
                {
                    throw new ApiException("Không tìm thấy nhóm học phần hợp lệ!", HttpStatusCode.InternalServerError);
                }

                return groupModule;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }


        public async Task<Data.Entities.GroupModule> Create(CreateGroupModuleRequest request)
        {
            try
            {
                //Validate lớp trùng năm, kỳ
                //...

                var groupModule = _mapper.Map<Data.Entities.GroupModule>(request);

                var userCurrent = await _userService.GetCurrentUserAsync();
                groupModule.CreatedAt = DateTime.Now;
                groupModule.CreatedBy = userCurrent?.Id;

                await _dbContext.GroupModules.AddAsync(groupModule);
                await _dbContext.SaveChangesAsync();

                return groupModule;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<Data.Entities.GroupModule> Edit(EditGroupModuleRequest request)
        {
            try
            {
                //Validate lớp trùng năm, kỳ
                //...

                var groupModule = await _dbContext.GroupModules.FindAsync(request.Id);

                if (groupModule == null)
                {
                    throw new ApiException("Không tìm thấy nhóm học phần hợp lệ!", HttpStatusCode.InternalServerError);
                }

                _mapper.Map(request, groupModule);
                var userCurrent = await _userService.GetCurrentUserAsync();
                groupModule.UpdatedAt = DateTime.Now;
                groupModule.CreatedBy = userCurrent?.Id;

                await _dbContext.SaveChangesAsync();

                return groupModule;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<Data.Entities.GroupModule> GenerateInvitationCode(GenerateInvitationCodeRequest request)
        {
            try
            {
                var groupModule = await _dbContext.GroupModules.FindAsync(request.Id);

                if (groupModule == null)
                {
                    throw new ApiException("Không tìm thấy nhóm học phần hợp lệ!", HttpStatusCode.InternalServerError);
                }

                var currentTime = DateTime.Now;

                if (request.Type == Constants.GroupModule.GetCode)
                {
                    if (groupModule.ExpiryTimeInvitation <= currentTime || groupModule.ExpiryTimeInvitation == null)
                    {
                        groupModule.InvitationCode = StringHelper.GenerateRandomCode(8);
                        groupModule.ExpiryTimeInvitation = currentTime.AddMinutes(15);

                        var userCurrent = await _userService.GetCurrentUserAsync();
                        groupModule.UpdatedAt = DateTime.Now;
                        groupModule.CreatedBy = userCurrent?.Id;
                    }
                }
                else
                {
                    groupModule.InvitationCode = StringHelper.GenerateRandomCode(8);
                    groupModule.ExpiryTimeInvitation = currentTime.AddMinutes(15);
                    var userCurrent = await _userService.GetCurrentUserAsync();
                    groupModule.UpdatedAt = DateTime.Now;
                    groupModule.CreatedBy = userCurrent?.Id;
                }

                await _dbContext.SaveChangesAsync();

                return groupModule;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<Data.Entities.GroupModule> Delete(int id)
        {
            try
            {
                var groupModule = await _dbContext.GroupModules.FindAsync(id);

                if (groupModule == null)
                {
                    throw new ApiException("Không tìm thấy nhóm học phần hợp lệ!", HttpStatusCode.InternalServerError);
                }

                var userCurrent = await _userService.GetCurrentUserAsync();
                groupModule.DeletedAt = DateTime.Now;
                groupModule.CreatedBy = userCurrent?.Id;

                await _dbContext.SaveChangesAsync();

                return groupModule;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<Data.Entities.GroupModule> Hide(int id)
        {
            try
            {
                var groupModule = await _dbContext.GroupModules.FindAsync(id);

                if (groupModule == null)
                {
                    throw new ApiException("Không tìm thấy nhóm học phần hợp lệ!", HttpStatusCode.InternalServerError);
                }

                groupModule.Status = false;

                var userCurrent = await _userService.GetCurrentUserAsync();
                groupModule.UpdatedAt = DateTime.Now;
                groupModule.UpdatedBy = userCurrent?.Id;

                await _dbContext.SaveChangesAsync();

                return groupModule;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        public byte[] ExportStudents(List<Student> students)
        {
            try
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add(ExportFile.ListStudentExcelTab);

                    worksheet.Cells[1, 1, 1, 7].Style.Font.Bold = true;
                    worksheet.Cells[1, 1, 1, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, 1, 1, 7].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189)); // Màu xanh dương
                    worksheet.Cells[1, 1, 1, 7].Style.Font.Color.SetColor(Color.White);

                    worksheet.Cells[1, 1].Value = "MSSV";
                    worksheet.Cells[1, 2].Value = "Họ và tên";
                    worksheet.Cells[1, 3].Value = "Email";
                    worksheet.Cells[1, 4].Value = "Số điện thoại";
                    worksheet.Cells[1, 5].Value = "Giới tính";
                    worksheet.Cells[1, 6].Value = "Ngày sinh";
                    worksheet.Cells[1, 7].Value = "Địa chỉ";


                    int row = 2;
                    foreach (var student in students)
                    {
                        worksheet.Cells[row, 1].Value = student.StudentId;
                        worksheet.Cells[row, 2].Value = student.User.Name;
                        worksheet.Cells[row, 3].Value = student.User.Email;
                        worksheet.Cells[row, 4].Value = PhoneHelper.FormatPhoneNumber(student.User.PhoneNumber);
                        worksheet.Cells[row, 5].Value = student.User.Sex != null ? (student.User.Sex == Constants.UserInfo.MaleKey ? Constants.UserInfo.MaleValue : Constants.UserInfo.FemaleValue) : "";
                        worksheet.Cells[row, 6].Value = student.User.BirthDay != null ? student.User.BirthDay.Value.ToString("dd/MM/yyyy") : "";
                        worksheet.Cells[row, 7].Value = student.User.Address != null ? student.User.Address : "";

                        row++;
                    }

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

        public async Task<byte[]> ExportScoreStudents(int groupModuleId)
        {
            var students = await _dbContext.StudentGroupModules
            .Include(sgm => sgm.Student)
            .ThenInclude(s => s.User)
            .Where(sgm => sgm.GroupModuleId == groupModuleId && sgm.Student.DeletedAt == null)
            .Select(sgm => sgm.Student)
            .ToListAsync();

            var exams = _dbContext.ExamGroupModules
            .Include(egm => egm.Exam)
            .ThenInclude(er => er.ExamResults)
            .Where(egm => egm.GroupModuleId == groupModuleId && egm.Exam.DeletedAt == null)
            .Select(egm => egm.Exam)
            .ToList();

            try
            {
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add(ExportFile.ListScoreStudentExcelTab);

                    worksheet.Cells[1, 1].Value = "MSSV";
                    worksheet.Cells[1, 2].Value = "Họ và tên";

                    int examColumnIndex = 3;
                    foreach (var exam in exams)
                    {
                        worksheet.Cells[1, examColumnIndex].Value = exam.Title;
                        examColumnIndex++;
                    }

                    worksheet.Cells[1, 1, 1, exams.Count + 2].Style.Font.Bold = true;
                    worksheet.Cells[1, 1, 1, exams.Count + 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, 1, 1, exams.Count + 2].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(79, 129, 189)); // Màu xanh dương
                    worksheet.Cells[1, 1, 1, exams.Count + 2].Style.Font.Color.SetColor(Color.White);

                    int row = 2;
                    foreach (var student in students)
                    {
                        worksheet.Cells[row, 1].Value = student.StudentId;
                        worksheet.Cells[row, 2].Value = student.User.Name;

                        examColumnIndex = 3;
                        foreach (var exam in exams)
                        {
                            var examScore = exam.ExamResults.FirstOrDefault(x => x.StudentId == student.Id) != null ? exam.ExamResults.FirstOrDefault(x => x.StudentId == student.Id).Score : 0;
                            worksheet.Cells[row, examColumnIndex].Value = examScore;
                            examColumnIndex++;
                        }

                        row++;
                    }

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

        public async Task<StudentGroupModule> AddStudentGroupModule(AddStudentGroupModuleRequest request)
        {
            try
            {
                //Validate
                var student = await _dbContext.Students.FirstOrDefaultAsync(x => x.StudentId == request.StudentId);

                if (student == null)
                {
                    throw new ApiException("Mã sinh viên không tồn tại trong hệ thống!", HttpStatusCode.InternalServerError);
                }

                var groupModule = await _dbContext.GroupModules.FindAsync(request.GroupModuleId);

                if (groupModule == null)
                {
                    throw new ApiException("Mã nhóm học phần không tồn tại trong hệ thống!", HttpStatusCode.InternalServerError);
                }

                var studentGroupModule = await _dbContext.StudentGroupModules.FirstOrDefaultAsync(x => x.GroupModuleId == request.GroupModuleId && x.StudentId == student.Id);

                if (studentGroupModule != null)
                {
                    throw new ApiException("Sinh viên đã tham gia lớp học!", HttpStatusCode.InternalServerError);
                }

                //Add student
                var studentGroupModuleCreate = new StudentGroupModule()
                {
                    StudentId = student.Id,
                    GroupModuleId = request.GroupModuleId,
                    JoinDate = DateTime.Now
                };

                await _dbContext.StudentGroupModules.AddAsync(studentGroupModuleCreate);
                await _dbContext.SaveChangesAsync();

                return studentGroupModuleCreate;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task ImportStudentsExcel(ImportStudentsGroupModuleRequest request)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    using (var stream = new MemoryStream())
                    {
                        await request.File.CopyToAsync(stream);

                        using (var package = new ExcelPackage(stream))
                        {
                            var worksheet = package.Workbook.Worksheets[0];
                            var rowCount = worksheet.Dimension.Rows;

                            for (int row = 2; row <= rowCount; row++)
                            {
                                var studentId = worksheet.Cells[row, 1].Value?.ToString().Trim();
                                var name = worksheet.Cells[row, 2].Value?.ToString().Trim();
                                var email = worksheet.Cells[row, 3].Value?.ToString().Trim();
                                var numberPhone = worksheet.Cells[row, 4].Value?.ToString().Trim();
                                var sexStr = worksheet.Cells[row, 4].Value?.ToString().Trim();
                                int? sex = null;
                                if (!string.IsNullOrWhiteSpace(sexStr))
                                {
                                    sex = sexStr == Constants.UserInfo.MaleValue ? Constants.UserInfo.MaleKey : Constants.UserInfo.FemaleKey;
                                }
                                var birthdayStr = worksheet.Cells[row, 5].Value?.ToString().Trim();
                                DateTime? birthday = null;
                                if (!string.IsNullOrWhiteSpace(birthdayStr))
                                {
                                    birthday = DateTime.TryParseExact(birthdayStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate)
                                               ? parsedDate
                                               : (DateTime?)null;
                                };
                                var address = worksheet.Cells[row, 6].Value?.ToString().Trim();

                                if (string.IsNullOrEmpty(studentId))
                                {
                                    throw new ApiException("Dữ liệu trường mã sinh viên dòng" + row + " không được để trống!", HttpStatusCode.BadRequest);
                                }

                                if (string.IsNullOrEmpty(email))
                                {
                                    throw new ApiException("Dữ liệu trường email dòng " + row + " không được để trống!", HttpStatusCode.BadRequest);
                                }

                                if (string.IsNullOrEmpty(numberPhone))
                                {
                                    throw new ApiException("Dữ liệu trường số điện thoại " + row + " không được để trống!", HttpStatusCode.BadRequest);
                                }

                                var student = await _dbContext.Students.FirstOrDefaultAsync(x => x.StudentId == studentId);

                                if (student != null)
                                {
                                    var studentGroupModule = await _dbContext.StudentGroupModules.FirstOrDefaultAsync(x => x.GroupModuleId == request.GroupModuleId && x.StudentId == student.Id);

                                    if (studentGroupModule != null)
                                    {
                                        throw new ApiException("Sinh viên có mã " + student.StudentId + " đã tham gia lớp học!", HttpStatusCode.InternalServerError);
                                    }
                                    else
                                    {
                                        studentGroupModule = new StudentGroupModule()
                                        {
                                            StudentId = student.Id,
                                            GroupModuleId = request.GroupModuleId,
                                            JoinDate = DateTime.Now
                                        };
                                    }

                                    await _dbContext.StudentGroupModules.AddAsync(studentGroupModule);
                                }
                                else
                                {
                                    var existingUser = _dbContext.Users.FirstOrDefault(user =>
                                    (user.PhoneNumber == numberPhone && user.PhoneNumberConfirmed && !string.IsNullOrEmpty(numberPhone)) ||
                                    (user.Email == email && user.EmailConfirmed && !string.IsNullOrEmpty(email)));

                                    if (existingUser != null)
                                    {
                                        string duplicateField = existingUser.PhoneNumber == numberPhone ? "Số điện thoại" : "Email";
                                        throw new ApiException($"{duplicateField} tại dòng {row} đã tồn tại trong hệ thống!", HttpStatusCode.BadRequest);
                                    }

                                    var user = new User()
                                    {
                                        Name = name,
                                        Status = true,
                                        UserName = studentId,
                                        NormalizedUserName = studentId.ToUpper(),
                                        Email = email,
                                        NormalizedEmail = email.ToUpper(),
                                        EmailConfirmed = false,
                                        PhoneNumber = string.IsNullOrEmpty(numberPhone) ? null : numberPhone,
                                        PhoneNumberConfirmed = false,
                                        BirthDay = birthday,
                                        Sex = sex,
                                        Address = address,
                                        AvatarUrl = ResourceConstant.DefaultAvatarUrl,
                                    };

                                    var result = await _userManager.CreateAsync(user, request.PasswordStudent);

                                    if (!result.Succeeded)
                                    {
                                        throw new ApiException($"Không thể tạo người dùng dòng {row}. Lỗi: {string.Join(", ", result.Errors)}", HttpStatusCode.BadRequest);
                                    }

                                    await _userManager.AddToRoleAsync(user, Constants.Role.Student);

                                    student = new Student()
                                    {
                                        StudentId = studentId,
                                        UserId = user.Id
                                    };

                                    await _dbContext.Students.AddAsync(student);

                                    var studentGroupModule = new StudentGroupModule()
                                    {
                                        StudentId = student.Id,
                                        GroupModuleId = request.GroupModuleId,
                                        JoinDate = DateTime.Now
                                    };

                                    await _dbContext.StudentGroupModules.AddAsync(studentGroupModule);

                                    var userCreationCountConfig = await _configService.GetConfigValue(ConfigConstant.UserCreationCount);
                                    var userCreationCount = int.Parse(userCreationCountConfig);
                                    await _configService.UpdateConfigValue(ConfigConstant.UserCreationCount, (userCreationCount + 1).ToString());
                                }
                            }
                        }
                    }

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
                }
            }
        }

        public async Task<bool> RemoveStudentGroupModule(RemoveStudentRequest request)
        {
            try
            {
                var groupModuleStudent = await _dbContext.StudentGroupModules.FirstOrDefaultAsync(x => x.StudentId == request.StudentId && x.GroupModuleId == request.GroupModuleId);

                if (groupModuleStudent == null)
                {
                    throw new ApiException("Không tìm thấy nhóm học phần hợp lệ!", HttpStatusCode.InternalServerError);
                }

                _dbContext.StudentGroupModules.Remove(groupModuleStudent);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<bool> RemoveMultipleStudentsFromGroupModule(RemoveMultipleStudentsRequest request)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var studentsToRemove = await _dbContext.StudentGroupModules
                        .Where(x => x.GroupModuleId == request.GroupModuleId && request.StudentIds.Contains(x.StudentId))
                        .ToListAsync();

                    if (studentsToRemove == null || !studentsToRemove.Any())
                    {
                        throw new ApiException("Không tìm thấy sinh viên trong nhóm học phần hợp lệ!", HttpStatusCode.InternalServerError);
                    }

                    _dbContext.StudentGroupModules.RemoveRange(studentsToRemove);
                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
                }
            }
        }

        public async Task<List<Exam>> GetExamsByGroupModule(int groupModuleId)
        {
            try
            {
                var exams = await _dbContext.ExamGroupModules
                .Include(egm => egm.Exam)
                .ThenInclude(er => er.ExamResults)
                .Where(egm => egm.GroupModuleId == groupModuleId && egm.Exam.DeletedAt == null)
                .Select(egm => egm.Exam)
                .ToListAsync();

                return exams;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<List<Notification>> GetNotificationsByGroupModule(int groupModuleId)
        {
            try
            {
                var notifications = await _dbContext.NotificationGroupModules
                .Include(egm => egm.Notification)
                .Where(egm => egm.GroupModuleId == groupModuleId && egm.Notification.DeletedAt == null)
                .Select(egm => egm.Notification)
                .ToListAsync();

                return notifications;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }



        public async Task<List<Data.Entities.GroupModule>> GetGroupModuleByUser(Guid userId)
        {
            try
            {
                var studentId = await _dbContext.Students
                                  .Where(s => s.UserId == userId)
                                  .Select(s => s.Id)
                                  .SingleOrDefaultAsync();

                if (studentId == null)
                {
                    return new List<Data.Entities.GroupModule>();
                }

                var groupModules = await _dbContext.StudentGroupModules
                                                    .Where(sgm => sgm.StudentId == studentId)
                                                    .Include(sgm => sgm.GroupModule)
                                                        .ThenInclude(gm => gm.ExamGroupModules)
                                                    .Select(sgm => sgm.GroupModule)
                                                    .ToListAsync();

                return groupModules;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }
    }
}
