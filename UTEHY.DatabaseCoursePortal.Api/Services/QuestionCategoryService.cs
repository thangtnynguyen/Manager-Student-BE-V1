using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using Twilio.Http;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Enums;
using UTEHY.DatabaseCoursePortal.Api.Exceptions;
using UTEHY.DatabaseCoursePortal.Api.Models.Banner;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Mail;
using UTEHY.DatabaseCoursePortal.Api.Models.QuestionCategory;
using UTEHY.DatabaseCoursePortal.Api.Models.Teacher;
using UTEHY.DatabaseCoursePortal.Api.Models.User;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class QuestionCategoryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public QuestionCategoryService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<QuestionCategoryDto>?> GetTree()
        {
            var query = _dbContext.QuestionCategories
                .Where(x => x.DeletedAt == null)
                .OrderByDescending(x => x.IsDefault)
                .AsQueryable();

            var categories = await query.ToListAsync();

            var categoryDtos = _mapper.Map<List<QuestionCategoryDto>>(categories);

            foreach (var categoryDto in categoryDtos)
            {
                categoryDto.QuestionCategories = GetChildCategories(categoryDto.Id);
            }

            return categoryDtos.Where(c => c.ParentQuestionCategoryId == null).ToList();
        }

        private List<QuestionCategoryDto>? GetChildCategories(int parentId)
        {
            var childCategoriesQuery = _dbContext.QuestionCategories
                .Where(x => x.ParentQuestionCategoryId == parentId && x.DeletedAt == null)
                .AsQueryable();

            var childCategories = childCategoriesQuery.ToList();

            if (childCategories.Any())
            {
                var childCategoryDtos = _mapper.Map<List<QuestionCategoryDto>>(childCategories);
                foreach (var childCategoryDto in childCategoryDtos)
                {
                    childCategoryDto.QuestionCategories = GetChildCategories(childCategoryDto.Id);
                }

                return childCategoryDtos;
            }

            return null;
        }

        public async Task<QuestionCategoryDto> Create(CreateQuestionCategoryRequest request)
        {
            var questionCategory = new QuestionCategory
            {
                Name = request.Name,
                Description = request.Description,
                ParentQuestionCategoryId = request.ParentQuestionCategoryId,
                Priority = request.Priority
            };

            await _dbContext.QuestionCategories.AddAsync(questionCategory);
            await _dbContext.SaveChangesAsync();

            var questionCategoryDto = _mapper.Map<QuestionCategoryDto>(questionCategory);
            return questionCategoryDto;
        }

        public async Task<QuestionCategoryDto> Edit(EditQuestionCategoryRequest request)
        {
            var questionCategory = await _dbContext.QuestionCategories.FindAsync(request.Id);

            if (questionCategory == null)
            {
                throw new Exception("Danh mục câu hỏi không tồn tại!");
            }

            _mapper.Map(request, questionCategory);

            await _dbContext.SaveChangesAsync();

            var questionCategoryDto = _mapper.Map<QuestionCategoryDto>(questionCategory);
            return questionCategoryDto;
        }

        public async Task<QuestionCategoryDto> Delete(DeleteRequest request)
        {
            var query = _dbContext.QuestionCategories.Include(x => x.Questions).Where(x => x.DeletedAt == null).AsQueryable();

            var questionCategory = await _dbContext.QuestionCategories.FindAsync(request.Id);

            if (questionCategory == null)
            {
                throw new ApiException("Câu hỏi không tồn tại!", HttpStatusCode.BadRequest);
            }

            questionCategory.DeletedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            var questionCategoryDto = _mapper.Map<QuestionCategoryDto>(questionCategory);

            return questionCategoryDto;
        }
    }
}
