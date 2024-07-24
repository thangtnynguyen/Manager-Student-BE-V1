using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Exceptions;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Notification;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class NotificationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserService _userService;
        private readonly IMapper _mapper;

        public NotificationService(ApplicationDbContext dbContext, IMapper mapper, UserService userService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<PagingResult<NotificationDto>> Get(GetNotificationRequest request)
        {
            try
            {
                var query = _dbContext.Notifications.Where(x => x.DeletedAt == null).AsQueryable();

                if (!string.IsNullOrEmpty(request.Title))
                {
                    query = query.Where(b => b.Title.ToLower().Contains(request.Title.ToLower()));
                }

                if (request.CreateBy != null)
                {
                    query = query.Where(b => b.CreatedBy == request.CreateBy);
                }
                int total = await query.CountAsync();

                if (request.PageIndex == null) request.PageIndex = 1;
                if (request.PageSize == null) request.PageSize = total;

                int totalPages = (int)Math.Ceiling((double)total / request.PageSize.Value);

                if (string.IsNullOrEmpty(request.SortBy) || request.SortBy == SortByConstant.Desc)
                {
                    query = request.OrderBy switch
                    {
                        OrderByConstant.Id or _ => query.OrderByDescending(b => b.Id),
                    };
                }
                else if (request.SortBy == SortByConstant.Asc)
                {
                    query = request.OrderBy switch
                    {
                        OrderByConstant.Id or _ => query.OrderBy(b => b.Id),
                    };
                }

                var items = await query
                    .Skip((request.PageIndex.Value - 1) * request.PageSize.Value)
                    .Take(request.PageSize.Value)
                    .Include(r => r.NotificationGroupModules)
                        .ThenInclude(rp => rp.GroupModule)
                    .ToListAsync();
                var notificationDtos = _mapper.Map<List<NotificationDto>>(items);

                var result = new PagingResult<NotificationDto>(notificationDtos, request.PageIndex.Value, request.PageSize.Value, request.SortBy, request.OrderBy, total, totalPages);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }
        public async Task<NotificationDto> Create(CreateNotificationRequest request)
        {
            try
            {
                var notification = _mapper.Map<Notification>(request);

                var userCurrent = await _userService.GetUserCurrentAsync();
                notification.CreatedAt = DateTime.Now;
                notification.CreatedBy = userCurrent?.Id;

                await _dbContext.Notifications.AddAsync(notification);
                await _dbContext.SaveChangesAsync();

                if (request.GroupModuleIds != null && request.GroupModuleIds.Any())
                {
                    foreach (var groupId in request.GroupModuleIds)
                    {
                        var notificationGroupModule = new NotificationGroupModule
                        {
                            NotificationId = notification.Id,
                            GroupModuleId = groupId
                        };

                        await _dbContext.NotificationGroupModules.AddAsync(notificationGroupModule);
                    }

                    await _dbContext.SaveChangesAsync();
                }

                return _mapper.Map<NotificationDto>(notification);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }
        public async Task<NotificationDto> Edit(EditNotificationRequest request)
        {
            try
            {
                var notification = await _dbContext.Notifications.FindAsync(request.Id);

                if (notification == null)
                {
                    throw new ApiException("Không tìm thấy thông báo  hợp lệ!", HttpStatusCode.InternalServerError);
                }

                _mapper.Map(request, notification);
                var userCurrent = await _userService.GetUserCurrentAsync();
                notification.UpdatedAt = DateTime.Now;
                notification.UpdatedBy = userCurrent?.Id;

                await _dbContext.SaveChangesAsync();

                return _mapper.Map<NotificationDto>(notification);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<NotificationDto> Delete(int id)
        {
            try
            {
                var notification = await _dbContext.Notifications.FindAsync(id);

                if (notification == null)
                {
                    throw new ApiException("Không tìm thấy thông báo hợp lệ!", HttpStatusCode.InternalServerError);
                }

                var userCurrent = await _userService.GetUserCurrentAsync();
                notification.DeletedAt = DateTime.Now;
                notification.UpdatedBy = userCurrent?.Id;

                await _dbContext.SaveChangesAsync();

                return _mapper.Map<NotificationDto>(notification);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
