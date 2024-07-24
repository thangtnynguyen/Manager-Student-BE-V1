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
using UTEHY.DatabaseCoursePortal.Api.Models.Tag;
using UTEHY.DatabaseCoursePortal.Api.Models.User;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class TagService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public TagService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<Tag>?> Get(GetTagRequestcs request)
        {
            var tags = await _dbContext.Tags
            .Where(x => (request.Type == null || x.Type == request.Type) && (request.Name == null || x.Name.ToLower().Contains(request.Name.ToLower())))
            .ToListAsync();

            return tags;
        }

        public async Task<Tag> Create(CreateTagRequest request)
        {
            try
            {
                var existingTag = _dbContext.Tags.FirstOrDefault(x => x.Name.Trim().ToLower() == request.Name.Trim().ToLower());

                if(existingTag != null) 
                {
                    throw new ApiException("Tồn tại thẻ tag, vui lòng chọn tên khác!", HttpStatusCode.InternalServerError);
                }

                var tag = _mapper.Map<Tag>(request);

                _dbContext.Tags.Add(tag);
                await _dbContext.SaveChangesAsync();

                return tag;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }
        public async Task<Tag> Edit(EditTagRequest request)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var tag = await _dbContext.Tags.FindAsync(request.Id);

                    if (tag == null)
                    {
                        throw new ApiException("Không tìm thấy tag có Id hợp lệ!", HttpStatusCode.BadRequest);
                    }

                    tag.Id = request.Id;

                    await _dbContext.SaveChangesAsync();

                    tag.Name = request.Name;
                    tag.Type = request.Type;

                    await _dbContext.SaveChangesAsync();

                    transaction.Commit();

                    var result = _mapper.Map<Tag>(tag);

                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    throw new ApiException("Có lỗi xảy ra trong quá trình xử lý!", HttpStatusCode.InternalServerError, ex);
                }
            }

        }





        public async Task<DeleteMultipleResult<int>> DeleteMultiple(List<int> tagIds)
        {
            var successfulIds = new List<int>();
            var failedIds = new List<int>();

            foreach (var tagId in tagIds)
            {
                var tag = await _dbContext.Tags.FindAsync(tagId);

                if (tag == null)
                {
                    failedIds.Add(tagId);
                    continue;
                }

                tag.DeletedAt = DateTime.Now;


                successfulIds.Add(tagId);
            }

            await _dbContext.SaveChangesAsync();

            var result = new DeleteMultipleResult<int>
            {
                SuccessfulItems = successfulIds,
                FailedItems = failedIds
            };

            return result;
        }
    }   
}
