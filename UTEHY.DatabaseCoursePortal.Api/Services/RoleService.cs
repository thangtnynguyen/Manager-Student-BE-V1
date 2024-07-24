using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Exceptions;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Permisson;
using UTEHY.DatabaseCoursePortal.Api.Models.Role;
using UTEHY.DatabaseCoursePortal.Api.Models.Role;
using Role = UTEHY.DatabaseCoursePortal.Api.Data.Entities.Role;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class RoleService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserService _userService;
        private readonly PermissionService _permissionService;


        public RoleService(ApplicationDbContext dbContext, IMapper mapper, UserService userService, PermissionService permissionService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userService = userService;
            _permissionService = permissionService;
        }

        public async Task<PagingResult<RoleDto>> Get(GetRoleRequest request)
        {
            try
            {
                var query = _dbContext.Roles.Where(x => x.DeletedAt == null).AsQueryable();

                if (!string.IsNullOrEmpty(request.Name))
                {
                    query = query.Where(b => b.Name.ToLower().Contains(request.Name.ToLower()));
                }

                if (!string.IsNullOrEmpty(request.Description))
                {
                    query = query.Where(b => b.Description.ToLower().Contains(request.Description.ToLower()));
                }

                int total = await query.CountAsync();

                if (request.PageIndex == null) request.PageIndex = 1;
                if (request.PageSize == null) request.PageSize = total;

                int totalPages = (int)Math.Ceiling((double)total / request.PageSize.Value);

                var roles = await query
                    .OrderByDescending(b => b.Id)
                    .Skip((request.PageIndex.Value - 1) * request.PageSize.Value)
                    .Take(request.PageSize.Value)
                    .Include(r => r.RolePermissions)
                        .ThenInclude(rp => rp.Permission)
                    .ToListAsync();

                var roleDtos = _mapper.Map<List<RoleDto>>(roles);

                foreach (var roleDto in roleDtos)
                {
                    roleDto.Permissions = await _permissionService.GetByRoleId(roleDto.Id);
                }

                var result = new PagingResult<RoleDto>(roleDtos, request.PageIndex.Value, request.PageSize.Value, total, totalPages);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }






        public async Task<Role> Create(CreateRoleRequest request)
        {
            try
            {
                request.NormalizedName = request.Name?.Replace(" ", "").ToLower();
                var role = _mapper.Map<Role>(request);

                var userCurrent = await _userService.GetCurrentUserAsync();
                role.CreatedAt = DateTime.Now;
                role.CreatedBy = userCurrent?.Id;

                await _dbContext.Roles.AddAsync(role);
                await _dbContext.SaveChangesAsync();

                if (request.PermissionIds != null && request.PermissionIds.Any())
                {
                    foreach (var permissionId in request.PermissionIds)
                    {
                        var rolePermission = new RolePermission
                        {
                            RoleId = role.Id,
                            PermessionId = permissionId
                        };

                        await _dbContext.RolePermissions.AddAsync(rolePermission);
                    }

                    await _dbContext.SaveChangesAsync();
                }

                return role;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }


        public async Task<Role> Edit(EditRoleRequest request)
        {
            try
            {
                request.NormalizedName = request.Name?.Replace(" ", "").ToLower();
                var role = await _dbContext.Roles.FindAsync(request.Id);

                if (role == null)
                {
                    throw new ApiException("Không tìm thấy quyền  hợp lệ!", HttpStatusCode.InternalServerError);
                }

                _mapper.Map(request, role);
                var userCurrent = await _userService.GetCurrentUserAsync();
                role.UpdatedAt = DateTime.Now;
                role.CreatedBy = userCurrent?.Id;

                await _dbContext.SaveChangesAsync();

                return role;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }


        public async Task<Role> Delete(Guid id)
        {
            try
            {
                var role = await _dbContext.Roles.FindAsync(id);

                if (role == null)
                {
                    throw new ApiException("Không tìm quyền hợp lệ!", HttpStatusCode.InternalServerError);
                }

                var userCurrent = await _userService.GetCurrentUserAsync();
                role.DeletedAt = DateTime.Now;
                role.CreatedBy = userCurrent?.Id;

                await _dbContext.SaveChangesAsync();

                return role;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        public async Task<List<Role>> DeleteMultiple(List<Guid?> ids)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var roles = await _dbContext.Roles
                                                   .Where(s => ids.Contains(s.Id) && s.DeletedAt == null)
                                                   .ToListAsync();

                    if (!roles.Any())
                    {
                        throw new ApiException("Không tìm thấy quyền nào hợp lệ để xoá.", HttpStatusCode.BadRequest);
                    }

                    foreach (var role in roles)
                    {
                        role.DeletedAt = DateTime.Now;
                    }

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return roles;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new ApiException($"Lỗi khi xoá các quyền: {ex.Message}", HttpStatusCode.InternalServerError, ex);
                }
            }
        }

    }
}






























//public async Task<PagingResult<RoleDto>> Get(GetRoleRequest request)
//{
//    var query = _dbContext.Roles.Where(x => x.DeletedAt == null).AsQueryable();

//    if (!string.IsNullOrEmpty(request.Name))
//    {
//        query = query.Where(b => b.Name.ToLower().Contains(request.Name.ToLower()));
//    }
//    if (!string.IsNullOrEmpty(request.Description))
//    {
//        query = query.Where(b => b.Description.ToLower().Contains(request.Description.ToLower()));
//    }

//    int total = await query.CountAsync();

//    if (request.PageIndex == null) request.PageIndex = 1;
//    if (request.PageSize == null) request.PageSize = total;

//    int totalPages = (int)Math.Ceiling((double)total / request.PageSize.Value);

//    var roles = await query
//        .OrderByDescending(b => b.Id)
//        .Skip((request.PageIndex.Value - 1) * request.PageSize.Value)
//        .Take(request.PageSize.Value)
//        .Include(r => r.RolePermissions)
//            .ThenInclude(rp => rp.Permission)
//        .ToListAsync();

//    var roleDtos = _mapper.Map<List<RoleDto>>(roles);

//    var result = new PagingResult<RoleDto>(roleDtos, request.PageIndex.Value, request.PageSize.Value, total, totalPages);

//    return result;
//}