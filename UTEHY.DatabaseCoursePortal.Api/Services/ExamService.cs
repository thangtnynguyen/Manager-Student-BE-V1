using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Exceptions;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Exam;
using UTEHY.DatabaseCoursePortal.Api.Models.Student;
using UTEHY.DatabaseCoursePortal.Api.Models.Question;

using UTEHY.DatabaseCoursePortal.Api.Models.ExamResult;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Drawing;
using System.Linq;
using UTEHY.DatabaseCoursePortal.Api.Models.QuestionAnswer;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class ExamService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly FileService _fileService;
        private readonly IMapper _mapper;
        private readonly UserService _userService;

        public ExamService(ApplicationDbContext dbContext, FileService fileService, IMapper mapper , UserService userService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<PagingResult<Exam>> Get(GetExamRequest request)
        {
            try
            {
                var query = _dbContext.Exams.Where(x => x.DeletedAt == null)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(request.Title))
                {
                    query = query.Where(b => b.Title.ToLower().Contains(request.Title.ToLower()));
                }
                if (request.CreateBy!=null)
                {
                    query = query.Where(b => b.CreatedBy==request.CreateBy);
                }

                //if (!string.IsNullOrEmpty(request.GroupModuleId.ToString()))
                //{
                //    query = query.Where(e => e.ExamGroupModules.Any(egm => egm.GroupModuleId == request.GroupModuleId));
                //}

                if (request.GroupModuleIds != null && request.GroupModuleIds.Any())
                {
                    query = query.Where(e => e.ExamGroupModules.Any(egm => request.GroupModuleIds.Contains((int)egm.GroupModuleId)));
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

                var result = new PagingResult<Exam>(items, request.PageIndex.Value, request.PageSize.Value, request.SortBy, request.OrderBy, total, totalPages);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<ExamDto> GetById(int id)
        {
            var exam = await _dbContext.Exams
                .Include(e => e.ExamQuestions)
                    .ThenInclude(eq => eq.Question)
                        .ThenInclude(q => q.QuestionAnswers) // Thêm dòng này để include các đáp án
                .Where(e => e.DeletedAt == null && e.Id == id)
                .FirstOrDefaultAsync();

            if (exam == null)
            {
                return null;
            }

            if (exam == null)
            {
                return null;
            }

            var examDto = new ExamDto
            {
                Id = (int)exam.Id,
                Title = exam.Title,
                Description = exam.Description,
                Duration = (TimeSpan)exam.Duration,
                StartTime = exam.StartTime,
                EndTime = exam.EndTime,
                NumberQuestionDifficult = exam.NumberQuestionDifficult,
                NumberQuestionEasy = exam.NumberQuestionEasy,
                NumberQuestionModerate = exam.NumberQuestionModerate,
                Type = exam.Type,
                IsSeeScore = exam.IsSeeScore,
                IsShowContent = exam.IsShowContent,
                Status = exam.Status,
                IsAllowChangeTab = exam.IsAllowChangeTab,
                IsMixQuestion = exam.IsMixQuestion,
                IsMixQuestionAnswer = exam.IsMixQuestionAnswer,

                Questions = new List<QuestionDto>()
            };

            foreach (var eq in exam.ExamQuestions)
            {
                var questionDto = new QuestionDto
                {
                    Id = eq.Question.Id,
                    Title = eq.Question.Title,
                    Feedback = eq.Question.Feedback,
                    Score = eq.Question.Score,
                    QuestionCategoryId = eq.Question.QuestionCategoryId,
                    Difficulty = eq.Question.Difficulty,
                    Type = eq.Question.Type,

                    Answers = eq.Question.QuestionAnswers.Select(a => new QuestionAnswerDto
                    {
                        Id = a.Id,
                        Content = a.Content,
                        IsCorrect = a.IsCorrect,
                        Score = a.Score
                    }).ToList()
                };

                examDto.Questions.Add(questionDto);
            }
            var random = new Random();
            examDto.Questions = examDto.Questions.OrderBy(q => random.Next()).ToList();

            return _mapper.Map<ExamDto>(examDto); 
        }


        //public async Task<ExamDto> GetById(int id)
        //{
        //    var options = new JsonSerializerOptions
        //    {
        //        ReferenceHandler = ReferenceHandler.Preserve,
        //    };



        //    var exam = await _dbContext.Exams
        //        .Include(e => e.ExamQuestions)
        //        .ThenInclude(eq => eq.Question)
        //        .Where(e => e.DeletedAt == null && e.Id == id)
        //        .FirstOrDefaultAsync();


        //    if (exam == null)
        //    {
        //        return null;
        //    }

        //    var examDto = new ExamDto
        //    {
        //        Id = (int)exam.Id,
        //        Title = exam.Title,
        //        Description = exam.Description,
        //        Duration = (TimeSpan)exam.Duration,
        //        StartTime = exam.StartTime,
        //        EndTime = exam.EndTime,
        //        NumberQuestionDifficult = exam.NumberQuestionDifficult,
        //        NumberQuestionEasy = exam.NumberQuestionEasy,
        //        NumberQuestionModerate = exam.NumberQuestionModerate,
        //        Type = exam.Type,
        //        IsSeeScore = exam.IsSeeScore,
        //        IsShowContent = exam.IsShowContent,
        //        Status= exam.Status,
        //        IsAllowChangeTab = exam.IsAllowChangeTab,
        //        IsMixQuestion = exam.IsMixQuestion,
        //        IsMixQuestionAnswer = exam.IsMixQuestionAnswer,

        //        Questions = new List<QuestionDto>() 
        //    };

        //    var examdto = _mapper.Map<ExamDto>(examDto);

        //    foreach (var eq in exam.ExamQuestions)
        //    {
        //        var questionDto = new QuestionDto
        //        {
        //            Id = eq.Question.Id,
        //            Title = eq.Question.Title,
        //            Feedback = eq.Question.Feedback,
        //            Score = eq.Question.Score,
        //            QuestionCategoryId = eq.Question.QuestionCategoryId,
        //            Difficulty = eq.Question.Difficulty,
        //            Type = eq.Question.Type
        //        };

        //        examDto.Questions.Add(questionDto); 
        //    }

        //    return examdto;
        //}







        //var newExam = new Exam
        //{
        //    Title =createExamRequest.Title,
        //    Description =createExamRequest.Description,
        //    Duration =createExamRequest.Duration,
        //    StartTime = createExamRequest.StartTime,
        //    EndTime = createExamRequest.EndTime,
        //    IsShowContent = createExamRequest.IsShowContent,
        //    IsSeeScore = createExamRequest.IsSeeScore,
        //    IsMixQuestion = createExamRequest.IsMixQuestion,
        //    IsMixQuestionAnswer = createExamRequest.IsMixQuestionAnswer,
        //    IsAllowChangeTab = createExamRequest.IsAllowChangeTab,
        //    Status = createExamRequest.Status,
        //    Type = createExamRequest.Type,
        //    NumberQuestionDifficult = createExamRequest.NumberQuestionDifficult,
        //    NumberQuestionModerate= createExamRequest.NumberQuestionModerate,
        //    NumberQuestionEasy = createExamRequest.NumberQuestionEasy,


        public async Task<Exam> Create(CreateExamRequest createExamRequest)
        {
            var userCurrent = await _userService.GetCurrentUserAsync();

            var exam = _mapper.Map<Exam>(createExamRequest);
            exam.CreatedAt = DateTime.Now;
            exam.CreatedBy = userCurrent?.Id;

        
                
            //};

            _dbContext.Exams.Add(exam);
            await _dbContext.SaveChangesAsync();

            //if (createExamRequest.IsMixQuestion == true)
            //{


            //}
            var createAuto = new GetQuestionAuto
            {
                NumberQuestionDifficult = exam.NumberQuestionDifficult,
                NumberQuestionEasy = exam.NumberQuestionEasy,
                NumberQuestionModerate = exam.NumberQuestionModerate,
            };
            var q = await AutoGetQuestion(createAuto);
            foreach (var questionDto in q)
            {
                var newExamQuestion = new ExamQuestion
                {
                    ExamId = (int)exam.Id,
                    QuestionId = questionDto.Id

                };

                _dbContext.ExamQuestions.Add(newExamQuestion);
            }


            foreach (var groupId in createExamRequest.GroupModuleIds)
            {
                var examGroupModule = new ExamGroupModule
                {
                    GroupModuleId = groupId,
                    ExamId = (int)exam.Id
                };

                _dbContext.ExamGroupModules.Add(examGroupModule);
            }

            await _dbContext.SaveChangesAsync();


            return exam;
        }





        public async Task<Exam> Delete(int id)
        {
            try
            {
                var exam = await _dbContext.Exams.FindAsync(id);

                if (exam == null)
                {
                    throw new ApiException("Không tìm thấy bài thi hợp lệ!", HttpStatusCode.InternalServerError);
                }

                var userCurrent = await _userService.GetCurrentUserAsync();
                exam.DeletedAt = DateTime.Now;
                exam.CreatedBy = userCurrent?.Id;

                await _dbContext.SaveChangesAsync();

                return exam;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<List<Exam>> DeleteMultiple(List<int?> ids)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var exams = await _dbContext.Exams
                                                   .Where(s => ids.Contains(s.Id) && s.DeletedAt == null)
                                                   .ToListAsync();

                    if (!exams.Any())
                    {
                        throw new ApiException("Không tìm thấy bài thi nào hợp lệ để xoá.", HttpStatusCode.BadRequest);
                    }

                    foreach (var exam in exams)
                    {
                        exam.DeletedAt = DateTime.Now;
                    }

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return exams;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new ApiException($"Lỗi khi xoá các bài thi: {ex.Message}", HttpStatusCode.InternalServerError, ex);
                }
            }
        }


        public async Task<ExamResultDto?> GetExamResult(int studentId, int examId)
        {
            try
            {
                var result = await _dbContext.ExamResults.FirstOrDefaultAsync(x => x.StudentId == studentId && x.ExamId == examId);

                return _mapper.Map<ExamResultDto>(result);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }



        public async Task<PagingResult<ExamResultDto>> GetExamResults(GetExamResultRequest request)
        {
            var studentQuery = from stg in _dbContext.StudentGroupModules
                               join st in _dbContext.Students on stg.StudentId equals st.Id
                               where !request.GroupModuleId.HasValue || stg.GroupModuleId == request.GroupModuleId.Value
                               select new { st, stg };

            var query = from st in studentQuery
                        join er in _dbContext.ExamResults.Where(x => x.ExamId == request.ExamId) on st.st.Id equals er.StudentId into examResults
                        from er in examResults.DefaultIfEmpty() // Đây là cách để thực hiện LEFT JOIN
                        select new
                        {
                            Student = st.st,
                            StudentGroupModule = st.stg,
                            ExamResult = er
                        };

            if (request.Type.HasValue)
            {
                if (request.Type.Value == 1)
                {
                    query = query.Where(x => x.ExamResult != null && x.ExamResult.Score != null);
                }
                else if (request.Type.Value == 2)
                {
                    query = query.Where(x => x.ExamResult == null || x.ExamResult.Score == null);
                }
            }

            if (!string.IsNullOrEmpty(request.StudentName))
            {
                query = query.Where(x => x.Student.User.Name.Contains(request.StudentName));
            }



            int total = await query.CountAsync();

            if (request.PageIndex == null) request.PageIndex = 1;
            if (request.PageSize == null) request.PageSize = total;

            int totalPages = (int)Math.Ceiling((double)total / request.PageSize.Value);

            if (string.IsNullOrEmpty(request.SortBy) || request.SortBy == SortByConstant.Desc)
            {
                query = request.OrderBy switch
                {
                    OrderByConstant.Score => query.OrderByDescending(b => b.ExamResult.Score),
                    OrderByConstant.NumberChangeTab => query.OrderByDescending(b => b.ExamResult.NumberChangeTab),
                    OrderByConstant.StudentId or _ => query.OrderByDescending(b => b.ExamResult.StudentId),


                };
            }
            else if (request.SortBy == SortByConstant.Asc)
            {
                query = request.OrderBy switch
                {
                    OrderByConstant.Score => query.OrderBy(b => b.ExamResult.Score),
                    OrderByConstant.NumberChangeTab => query.OrderBy(b => b.ExamResult.NumberChangeTab),
                    OrderByConstant.StudentId or _ => query.OrderBy(b => b.ExamResult.StudentId),

                };
            }


            var items = await query
                .Skip((request.PageIndex.Value - 1) * request.PageSize.Value)
                .Take(request.PageSize.Value)
                .ToListAsync();




            var examResultDtos = await query.Select(x => new ExamResultDto
            {
                Id= x.ExamResult.Id,
                StudentId = x.Student.StudentId,
                StudentName = x.Student.User.Name, 
                Score = x.ExamResult != null ? x.ExamResult.Score : null,
                StartTime = x.ExamResult.StartTime,
                DurationTime = x.ExamResult.DurationTime,
                NumberChangeTab=x.ExamResult.NumberChangeTab,
                NumberCorrectAnswers=x.ExamResult.Score
            })
            .Distinct()
            .ToListAsync();


            var result = new PagingResult<ExamResultDto>(examResultDtos, request.PageIndex.Value, request.PageSize.Value, total, totalPages);

            return result;
        }











        public async Task<ExamResult?> GetScoreExamStudent(int studentId, int examId)
        {
            try
            {
                var result = await _dbContext.ExamResults.FirstOrDefaultAsync(x => x.StudentId == studentId && x.ExamId == examId);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }








        public async Task<List<Question>?> AutoGetQuestion(GetQuestionAuto request)
        {
            try
            {
                List<Question> allQuestions = new List<Question>();

                // Lấy câu hỏi dễ
                var easyQuestions = await _dbContext.Questions
                    .Where(q => q.Difficulty == 1)
                    .OrderBy(q => Guid.NewGuid()) 
                    .Take(request.NumberQuestionEasy ?? 0)
                    .ToListAsync();

                allQuestions.AddRange(easyQuestions);

                // Lấy câu hỏi trung bình
                var moderateQuestions = await _dbContext.Questions
                    .Where(q => q.Difficulty == 2)
                    .OrderBy(q => Guid.NewGuid()) 
                    .Take(request.NumberQuestionModerate ?? 0)
                    .ToListAsync();

                allQuestions.AddRange(moderateQuestions);

                // Lấy câu hỏi khó
                var difficultQuestions = await _dbContext.Questions
                    .Where(q => q.Difficulty == 3)
                    .OrderBy(q => Guid.NewGuid()) 
                    .Take(request.NumberQuestionDifficult ?? 0)
                    .ToListAsync();

                allQuestions.AddRange(difficultQuestions);

                //var allQuestionDtos= _mapper.Map<QuestionDto>(allQuestions);

                return allQuestions;
            }
            catch (Exception ex)
            {
                // Xử lý lỗi
                return null;
            }
        }




        public async Task<CheckQuestionResult> CheckAnswers(List<CheckQuestionRequest> questionsToCheck)
        {
            var result = new CheckQuestionResult
            {
                CheckQuestions = new List<CheckQuestions>(),
                TotalScore = 0,
                TotalCountFalse = 0,
                TotalCountTrue = 0
            };

            foreach (var request in questionsToCheck)
            {
                var question = await _dbContext.Questions
                    .Include(q => q.QuestionAnswers)
                    .FirstOrDefaultAsync(q => q.Id == request.QuestionId);

                if (question != null)
                {
                    var correctAnswers = question.QuestionAnswers
                        .Where(a => a.IsCorrect);

                    var totalScore = correctAnswers.Sum(a => a.Score);
                    var isCorrect = correctAnswers.Any(a => a.Id == request.QuestionAnswerId);

                    var checkQuestion = new CheckQuestions
                    {
                        QuestionId = question.Id,
                        QuestionAnswerId = request.QuestionAnswerId,
                        QuestionAnswerCorrectId = correctAnswers.FirstOrDefault(a => a.IsCorrect)?.Id ?? 0,
                        QuestionTitle = question.Title,
                        QuestionAnswerContent = GetAnswerContent(question.QuestionAnswers, request.QuestionAnswerId),
                        QuestionAnswerCorrectContent = GetCorrectAnswerContent(correctAnswers),
                    };

                    result.CheckQuestions.Add(checkQuestion);
                    result.TotalScore += isCorrect ? totalScore : 0;
                    result.TotalCountFalse += isCorrect ? 0 : 1;
                    result.TotalCountTrue += isCorrect ? 1 : 0;
                }
            }



            return result;
        }

        private string GetAnswerContent(List<QuestionAnswer> answers, int answerId)
        {
            return answers.FirstOrDefault(a => a.Id == answerId)?.Content ?? "";
        }

        private string GetCorrectAnswerContent(IEnumerable<QuestionAnswer> correctAnswers)
        {
            return correctAnswers.FirstOrDefault()?.Content ?? "";
        }

        public async Task<ExamResult> AddExamResult(CreateExamResultRequest createExamResultRequests)
        {
            
            var examR = _mapper.Map<ExamResult>(createExamResultRequests);
            examR.CreatedAt = DateTime.Now;

            _dbContext.ExamResults.Add(examR);
            await _dbContext.SaveChangesAsync();

            return examR;

        }


        //public async Task<ExamResult?> GetExamResult(int studentId, int examId)
        //{
        //    //var result= await _dbContext.

        //    //return result;

        //    try
        //    {
        //        var result = await _dbContext.ExamResults.FirstOrDefaultAsync(x => x.StudentId == studentId && x.ExamId == examId);

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
        //    }
        //}

    }
}
