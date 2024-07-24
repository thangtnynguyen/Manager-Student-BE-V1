using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Text.Json;
using Twilio.Http;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Enums;
using UTEHY.DatabaseCoursePortal.Api.Exceptions;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Mail;
using UTEHY.DatabaseCoursePortal.Api.Models.Question;
using UTEHY.DatabaseCoursePortal.Api.Models.QuestionCategory;
using DocumentFormat.OpenXml.Office2010.Excel;
using Irony.Parsing;
using OfficeOpenXml;
using System.Globalization;
using UTEHY.DatabaseCoursePortal.Api.Models.GroupModule;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class QuestionService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly FileService _fileService;
        private readonly UserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly MailService _mailService;
        private readonly TwilioService _twilioService;
        private readonly IMapper _mapper;

        public QuestionService(ApplicationDbContext dbContext, FileService fileService, IMapper mapper, UserService userService, UserManager<User> userManager, MailService mailService, TwilioService twilioService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _mapper = mapper;
            _userService = userService;
            _userManager = userManager;
            _mailService = mailService;
            _twilioService = twilioService;
        }

        public async Task<PagingResult<QuestionDto>> Get(GetQuestionRequest request)
        {
            var query = _dbContext.Questions
                .Include(q => q.QuestionCategory)
                .Include(q => q.QuestionAnswers)
                .Include(q => q.Section)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.Title))
            {
                string search = request.Title.ToLower();
                query = query.Where(b => b.Title.ToLower().Contains(request.Title.ToLower()));
            }

            if (request.QuestionCategoryId != null)
            {
                query = query.Where(b => b.QuestionCategoryId == request.QuestionCategoryId);
            }

            if (request.SectionId != null)
            {
                query = query.Where(b => b.SectionId == request.SectionId);
            }

            if (request.Type != null)
            {
                query = query.Where(b => b.Type == request.Type);
            }

            if (request.Difficulty != null)
            {
                query = query.Where(b => b.Difficulty == request.Difficulty);
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
                if(request.SortBy == SortByConstant.Asc)
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
                if(request.OrderBy == OrderByConstant.Id && request.SortBy == SortByConstant.Asc)
                {
                    query = query.OrderBy(b => b.Id);
                }
                else if(request.OrderBy == OrderByConstant.Id && request.SortBy == SortByConstant.Desc)
                {
                    query = query.OrderByDescending(b => b.Id);
                }
                else if (request.OrderBy == OrderByConstant.Score && request.SortBy == SortByConstant.Asc)
                {
                    query = query.OrderBy(b => b.Score);
                }
                else if (request.OrderBy == OrderByConstant.Score && request.SortBy == SortByConstant.Desc)
                {
                    query = query.OrderByDescending(b => b.Score);
                }
            }

            var items = await query
            .Skip((request.PageIndex.Value - 1) * request.PageSize.Value)
            .Take(request.PageSize.Value)
            .ToListAsync();

            var itemsMapper = _mapper.Map<List<QuestionDto>>(items);

            var result = new PagingResult<QuestionDto>(itemsMapper, request.PageIndex.Value, request.PageSize.Value, total, totalPages);

            return result;
        }

        public async Task<QuestionDto> GetById(int id)
        {
            var question = await _dbContext.Questions
            .Include(x => x.QuestionAnswers)
            .Include(x => x.QuestionCategory)  
            .Include(x => x.Section)
            .Include(x => x.QuestionTags)
            .ThenInclude(x => x.Tag)
            .FirstOrDefaultAsync(x => x.Id == id);

            var result = _mapper.Map<QuestionDto>(question);

            //var questionDto = new QuestionDto()
            //{
            //    Id = question.Id,
            //    Title = question.Title,
            //    Feedback = question.Feedback,
            //    Score = question.Score,
            //    QuestionCategoryId = question.QuestionCategoryId,
            //    Difficulty = question.Difficulty,
            //    Type = question.Type,
            //    QuestionCategoryName = question.QuestionCategory.Name,
            //    Section = question.Section,
            //    QuestionAnswers = question.QuestionAnswers,

            //};

            //var tagIds = await _dbContext.QuestionTags.Where(x => x.QuestionId == question.Id).Select(x => x.TagId).ToListAsync();

            //var tags = new List<Tag>() { };

            //if(tagIds.Count > 0)
            //{
            //    foreach (var tag in tagIds)
            //    {
            //        var newTag = await _dbContext.Tags.FirstOrDefaultAsync(x => x.Id == tag);

            //        tags.Add(newTag);
            //    }
            //}

            //var options = new JsonSerializerOptions
            //{
            //    ReferenceHandler = ReferenceHandler.Preserve
            //};

            //var questionDto = JsonSerializer.Deserialize<QuestionDto>(
            //    JsonSerializer.Serialize(question, options)
            //);

            return result;
        }

        public async Task<bool> Create(CreateQuestionRequest request)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var question = _mapper.Map<Question>(request);

                    _dbContext.Questions.Add(question);
                    _dbContext.SaveChanges();

                    if (request.QuestionAnswers != null && request.QuestionAnswers.Any())
                    {
                        var questionAnswersEntities = request.QuestionAnswers
                            .Select(answerDto => _mapper.Map<QuestionAnswer>(answerDto))
                            .ToList();

                        foreach (var answerEntity in questionAnswersEntities)
                        {
                            answerEntity.QuestionId = question.Id;
                        }

                        _dbContext.QuestionAnswers.AddRange(questionAnswersEntities);
                    }

                    if (request.TagIds != null && request.TagIds.Any())
                    {
                        var questionTags = request.TagIds
                            .Select(tagId => new QuestionTag { QuestionId = question.Id, TagId = tagId })
                            .ToList();

                        _dbContext.QuestionTags.AddRange(questionTags);
                    }

                    _dbContext.SaveChanges();
                    transaction.Commit();

                    //var questionDto = _mapper.Map<QuestionDto>(question);

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    throw new ApiException("Có lỗi xảy ra trong quá trình xử lý!", HttpStatusCode.InternalServerError, ex);
                }
            }
        }

        public async Task<bool> Edit(EditQuestionRequest request)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var question = await _dbContext.Questions.FindAsync(request.Id);

                    if (question == null)
                    {
                        throw new ApiException("Không tìm thấy câu hỏi có ID tương ứng.", HttpStatusCode.NotFound);
                    }

                    _mapper.Map(request, question);

                    var oldAnswers = await _dbContext.QuestionAnswers
                        .Where(qa => qa.QuestionId == question.Id)
                        .ToListAsync();

                    _dbContext.QuestionAnswers.RemoveRange(oldAnswers);

                    if (request.QuestionAnswers != null && request.QuestionAnswers.Any())
                    {
                        var newAnswers = _mapper.Map<List<QuestionAnswer>>(request.QuestionAnswers);

                        foreach (var answerEntity in newAnswers)
                        {
                            answerEntity.QuestionId = question.Id;
                            answerEntity.Question = null;
                        }

                        _dbContext.QuestionAnswers.AddRange(newAnswers);
                    }

                    var oldTags = await _dbContext.QuestionTags
                        .Where(qt => qt.QuestionId == question.Id)
                        .ToListAsync();

                    _dbContext.QuestionTags.RemoveRange(oldTags);

                    if (request.TagIds != null && request.TagIds.Any())
                    {
                        var newTags = request.TagIds
                            .Select(tagId => new QuestionTag { QuestionId = question.Id, TagId = tagId })
                            .ToList();

                        _dbContext.QuestionTags.AddRange(newTags);
                    }

            question.UpdatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new ApiException("Có lỗi xảy ra trong quá trình xử lý!", HttpStatusCode.InternalServerError, ex);
                }
            }
        }


        public async Task<QuestionDto> Delete(int id)
        {
            var question = await _dbContext.Questions.FindAsync(id);

            if (question == null)
            {
                throw new ApiException("Không tìm thấy câu hỏiu có Id hợp lệ!", HttpStatusCode.BadRequest);
            }

            question.DeletedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            //var questionAnswers = await _dbContext.QuestionAnswers.FirstOrDefaultAsync(x => x.QuestionId == question.Id);

            //if (questionAnswers == null)
            //{
            //    throw new ApiException("Không tìm thấy câu trả lời có Id hợp lệ!", HttpStatusCode.BadRequest);
            //}


            var questionAnswers = await _dbContext.QuestionAnswers.Where(x => x.QuestionId == question.Id).ToListAsync();

            //if (questionAnswers == null || questionAnswers.Count == 0)
            //{
            //    throw new ApiException("Đã xóa câu hỏi, câu hỏi này không có câu trả lời!", HttpStatusCode.BadRequest);
            //}

            if (questionAnswers != null && questionAnswers.Count > 0)
            {
                DateTime now = DateTime.Now;

                foreach (var questionAnswer in questionAnswers)
                {
                    questionAnswer.DeletedAt = now;
                }
            }
           

            await _dbContext.SaveChangesAsync();

            var result = _mapper.Map<QuestionDto>(question);

            return result;
        }

        public async Task<bool> DeleteMultiple(List<int?> questionIds)
        {
            if (questionIds == null || !questionIds.Any())
            {
                throw new ApiException("Danh sách ID câu hỏi rỗng.", HttpStatusCode.BadRequest);
            }

            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var questions = await _dbContext.Questions
                        .Where(q => questionIds.Contains(q.Id) && q.DeletedAt == null)
                        .ToListAsync();

                    if (!questions.Any())
                    {
                        throw new ApiException("Không tìm thấy câu hỏi nào hợp lệ để xoá.", HttpStatusCode.BadRequest);
                    }

                    foreach (var question in questions)
                    {
                        question.DeletedAt = DateTime.Now;
                    }

                    await _dbContext.QuestionAnswers
                        .Where(qa => questionIds.Contains(qa.QuestionId) && qa.DeletedAt == null)
                        .ForEachAsync(qa => qa.DeletedAt = DateTime.Now);

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new ApiException($"Lỗi khi xoá các câu hỏi: {ex.Message}", HttpStatusCode.InternalServerError, ex);
                }
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












        public async Task ImportQuestionsExcel(ImportQuestionsRequest request)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    using (var stream = new MemoryStream())
                    {
                        var userCurrent = await _userService.GetCurrentUserAsync();

                        await request.File.CopyToAsync(stream);

                        using (var package = new ExcelPackage(stream))
                        {
                            var worksheet = package.Workbook.Worksheets[0];
                            var rowCount = worksheet.Dimension.End.Row;



                            for (int row = 2; row <= rowCount; row++) 
                            {
                                var Title = worksheet.Cells[row, 1].Value?.ToString().Trim();
                                var Feedback = worksheet.Cells[row, 2].Value?.ToString().Trim();
                                var Score = worksheet.Cells[row, 3].Value?.ToString().Trim();
                                var UserId = worksheet.Cells[row, 4].Value?.ToString().Trim();
                                var QuestionCategoryId = worksheet.Cells[row, 5].Value?.ToString().Trim();
                                var Difficulty = worksheet.Cells[row, 6].Value?.ToString().Trim();
                                var Type = worksheet.Cells[row, 7].Value?.ToString().Trim();
                                var SectionId = worksheet.Cells[row, 8].Value?.ToString().Trim();


                                if (string.IsNullOrEmpty(Title))
                                {
                                    throw new ApiException("Dữ liệu trường tiêu đề câu hỏi dòng " + row + " không được để trống!", HttpStatusCode.BadRequest);
                                }

                                if (string.IsNullOrEmpty(Feedback))
                                {
                                    throw new ApiException("Dữ liệu trường phản hồi câu hỏi dòng " + row + " không được để trống!", HttpStatusCode.BadRequest);
                                }

                                if (string.IsNullOrEmpty(Score))
                                {
                                    throw new ApiException("Dữ liệu trường điểm số câu hỏi " + row + " không được để trống!", HttpStatusCode.BadRequest);
                                }

                                Question question = new Question
                                {
                                    Title = Title,
                                    Feedback = Feedback,
                                    Score=int.Parse(Score),
                                    QuestionCategoryId=int.Parse(QuestionCategoryId),
                                    Difficulty=int.Parse(Difficulty),
                                    Type=int.Parse(Type),
                                    SectionId=int.Parse(SectionId),
                                    CreatedAt = DateTime.Now,
                                    CreatedBy = userCurrent?.Id,
                                };

                                _dbContext.Questions.Add(question);
                                await _dbContext.SaveChangesAsync();

                                for (int col = 9; col <= 13; col++) 
                                {
                                    if (worksheet.Cells[row, col].Value != null)
                                    {
                                        QuestionAnswer answer = new QuestionAnswer
                                        {
                                            Content = worksheet.Cells[row, col].Value.ToString(),
                                            IsCorrect = (col - 8) == Convert.ToInt32(worksheet.Cells[row, 14].Value),
                                            Score = question.Score,
                                            QuestionId = question.Id,
                                            CreatedAt = DateTime.Now,
                                            CreatedBy = userCurrent?.Id,
                                        };

                                        _dbContext.QuestionAnswers.Add(answer);
                                    }
                                }
                                await _dbContext.SaveChangesAsync();
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




    }
}
