using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Lesson;
using UTEHY.DatabaseCoursePortal.Api.Models.Role;
using UTEHY.DatabaseCoursePortal.Api.Models.Role;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers.Admin
{
    [Route("api/admin/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("get")]
        public async Task<ApiResult<PagingResult<RoleDto>>> Get([FromQuery] GetRoleRequest request)
        {
            var result = await _roleService.Get(request);

            return new ApiResult<PagingResult<RoleDto>>()
            {
                Status = true,
                Message = "Lấy thông tin danh vai trò thành công!",
                Data = result
            };
        }


        [HttpPost("create")]
        public async Task<ApiResult<Role>> Create([FromBody] CreateRoleRequest request)
        {
            var result = await _roleService.Create(request);

            return new ApiResult<Role>()
            {
                Status = true,
                Message = "Thêm role thành công!",
                Data = result
            };
        }

        [HttpPost("edit")]
        public async Task<ApiResult<Role>> Edit([FromForm] EditRoleRequest request)
        {
            var result = await _roleService.Edit(request);

            return new ApiResult<Role>()
            {
                Status = true,
                Message = "Cập nhập thành công!",
                Data = result
            };
        }

        [HttpPost("delete")]
        public async Task<ApiResult<Role>> Delete([FromBody] EntityIdentityRequest<Guid> request)
        {
            var result = await _roleService.Delete(request.Id);

            return new ApiResult<Role>()
            {
                Status = true,
                Message = "Đã xóa!",
                Data = result
            };
        }

        [HttpPost("delete-multiple")]
        public async Task<ApiResult<List<Role>>> DeleteMultiple([FromBody] ListEntityIdentityRequest<Guid?> request)
        {
            var result = await _roleService.DeleteMultiple(request.Ids);

            return new ApiResult<List<Role>>()
            {
                Status = true,
                Message = "Đã xóa các role",
                Data = result
            };
        }

    }
}
