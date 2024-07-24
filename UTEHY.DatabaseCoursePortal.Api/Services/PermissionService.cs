using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Twilio.Http;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Exceptions;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Permisson;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class PermissionService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;


        public PermissionService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        private List<PermissionDto> GetChildren(int parentId)
        {
            var children = _dbContext.Permissions
                .Where(p => p.ParentPermissionId == parentId)
                .ToList();

            var childDtos = new List<PermissionDto>();

            foreach (var childPermission in children)
            {
                var childDto = _mapper.Map<PermissionDto>(childPermission);
                childDto.Children = GetChildren(childPermission.Id);
                childDtos.Add(childDto);
            }

            return childDtos;
        }


        public async Task<PagingResult<PermissionDto>> Get(GetPermissionRequest request)
        {
            try
            {
                var permissions = await GetRecursive(null);

                var total = permissions.Count;

                if (request.PageIndex == null) request.PageIndex = 1;
                if (request.PageSize == null) request.PageSize = total;

                int totalPages = (int)Math.Ceiling((double)total / request.PageSize.Value);

                if (string.IsNullOrEmpty(request.SortBy) || request.SortBy == SortByConstant.Desc)
                {
                    permissions = request.OrderBy switch
                    {
                        OrderByConstant.Id or _ => permissions.OrderByDescending(p => p.Id).ToList(),
                    };
                }
                else if (request.SortBy == SortByConstant.Asc)
                {
                    permissions = request.OrderBy switch
                    {
                        OrderByConstant.Id or _ => permissions.OrderBy(p => p.Id).ToList(),
                    };
                }

                var items = permissions
                    .Skip((request.PageIndex.Value - 1) * request.PageSize.Value)
                    .Take(request.PageSize.Value)
                    .ToList();

                var result = new PagingResult<PermissionDto>(items, request.PageIndex.Value, request.PageSize.Value, request.SortBy, request.OrderBy, total, totalPages);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        private async Task<List<PermissionDto>> GetRecursive(int? parentPermissionId)
        {
            var children = _dbContext.Permissions
                 .Where(p => p.ParentPermissionId == parentPermissionId)
                 .ToList();

            var childDtos = new List<PermissionDto>();

            foreach (var childPermission in children)
            {
                var childDto = _mapper.Map<PermissionDto>(childPermission);
                childDto.Children = GetChildren(childPermission.Id);
                childDtos.Add(childDto);
            }

            return childDtos;
        }






        //theo roleID
        public async Task<List<PermissionDto>> GetByRoleId(Guid roleId)
        {
            try
            {
                var rolePermissions = await _dbContext.RolePermissions
                    .Where(rp => rp.RoleId == roleId)
                    .Include(rp => rp.Permission)
                    .ToListAsync();

                var permissions = rolePermissions.Select(rp => rp.Permission);

                var result = await GetRecursive(null, permissions);

                return result;
            }
            catch (Exception ex)
            {
                throw new ApiException(ex.Message, HttpStatusCode.InternalServerError, ex);
            }
        }

        private async Task<List<PermissionDto>> GetRecursive(int? parentPermissionId, IEnumerable<Data.Entities.Permission> allPermissions)
        {
            var children = allPermissions
                .Where(p => p.ParentPermissionId == parentPermissionId)
                .ToList();

            var childDtos = new List<PermissionDto>();

            foreach (var childPermission in children)
            {
                var childDto = _mapper.Map<PermissionDto>(childPermission);
                childDto.Children = await GetRecursive(childPermission.Id, allPermissions);
                childDtos.Add(childDto);
            }

            return childDtos;
        }







    }
}
