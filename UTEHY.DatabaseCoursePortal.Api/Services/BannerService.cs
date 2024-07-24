using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Twilio.Http;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Exceptions;
using UTEHY.DatabaseCoursePortal.Api.Models.Banner;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Student;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class BannerService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly FileService _fileService;
        private readonly IMapper _mapper;

        public BannerService(ApplicationDbContext dbContext, FileService fileService, IMapper mapper)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<PagingResult<Banner>> Get(GetBannerRequest request)
        {
            var query = _dbContext.Banners.AsQueryable();

            if (!string.IsNullOrEmpty(request.Type))
            {
                query = query.Where(b => b.Type == request.Type);
            }

            if (!string.IsNullOrEmpty(request.Place))
            {
                query = query.Where(b => b.Place == request.Place);
            }

            if (!string.IsNullOrEmpty(request.Title))
            {
                string search = request.Title.ToLower();
                query = query.Where(b => b.Title.ToLower().Contains(request.Title.ToLower()));
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

            var result = new PagingResult<Banner>(items, request.PageIndex.Value, request.PageSize.Value,request.SortBy,request.OrderBy, total, totalPages);

            return result;
        }

        public async Task<Banner> GetById(int id)
        {
            try
            {
                var banner = await _dbContext.Banners.FindAsync(id);

                if (banner == null)
                {
                    throw new ApiException("Không tìm thấy banner hợp lệ!", HttpStatusCode.InternalServerError);
                }

                return banner;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<Banner> Create(CreateBannerRequest request)
        {
            if (request.ImageFile?.Length > 0)
            {
                request.Image = await _fileService.UploadFileAsync(request.ImageFile, PathFolder.Banner);
            }

            var banner = _mapper.Map<Banner>(request);
            banner.CreatedAt = DateTime.Now;

            await _dbContext.Banners.AddAsync(banner);
            await _dbContext.SaveChangesAsync();

            return banner;
        }

        public async Task<Banner> Edit(EditBannerRequest request)
        {
            var banner = await _dbContext.Banners.FindAsync(request.Id);

            if (banner == null)
            {
                throw new Exception("Banner không tồn tại!");
            }

            if (request.ImageFile?.Length > 0)
            {
                request.Image = await _fileService.UploadFileAsync(request.ImageFile, PathFolder.Banner);
                await _fileService.DeleteFileAsync(banner.Image);
            }
            else
            {
                request.Image = banner.Image;
            }

            _mapper.Map(request, banner);
            banner.UpdatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return banner;
        }

        public async Task<Banner> Delete(int id)
        {
            var banner = await _dbContext.Banners.FindAsync(id);

            if (banner == null)
            {
                throw new Exception("Banner không tồn tại!");
            }

            banner.DeletedAt = DateTime.Now;

            _dbContext.Banners.Remove(banner);

            await _fileService.DeleteFileAsync(banner.Image);

            await _dbContext.SaveChangesAsync();

            return banner;
        }

        public async Task<List<Banner>> DeleteMultiple(List<int> ids)
        {
            var banners = await _dbContext.Banners.Where(x => ids.Contains(x.Id)).ToListAsync();

            var invalidIds = ids.Except(banners.Select(b => b.Id)).ToList();

            if (invalidIds.Any())
            {
                throw new Exception($"Danh sách Ids banner không tồn tại: {string.Join(", ", invalidIds)}");
            }

            _dbContext.Banners.RemoveRange(banners);

            var fileUrls = banners.Select(banner => banner.Image).ToList();

            await _fileService.DeleteFilesAsync(fileUrls);

            await _dbContext.SaveChangesAsync();

            return banners;
        }
    }
}
