using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Permisson;
using UTEHY.DatabaseCoursePortal.Api.Models.Role;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers.Admin
{
    [Route("api/admin/permission")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly PermissionService _permissionService;

        public PermissionController(PermissionService PermissionService)
        {
            _permissionService = PermissionService;
        }

        [HttpGet("get")]
        public async Task<ApiResult<PagingResult<PermissionDto>>> Get([FromQuery] GetPermissionRequest request)
        {
            var result = await _permissionService.Get(request);

            return new ApiResult<PagingResult<PermissionDto>>()
            {
                Status = true,
                Message = "Lấy thông tin danh quyền thành công thành công!",
                Data = result
            };
        }

        [HttpGet("get-by-role")]
        public async Task<ApiResult<List<PermissionDto>>> GetByRoldID([FromQuery] Guid request)
        {
            var result = await _permissionService.GetByRoleId(request);

            return new ApiResult<List<PermissionDto>>()
            {
                Status = true,
                Message = "Lấy thông tin danh quyền thành công thành công!",
                Data = result
            };
        }
    }
}
