using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Group;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly GroupService _groupService;

        public GroupController(GroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet("get")]
        public async Task<ApiResult<PagingResult<Group>>> Get([FromQuery] GetGroupRequest request)
        {
            var result = await _groupService.Get(request);

            return new ApiResult<PagingResult<Group>>()
            {
                Status = true,
                Message = "Lấy thông tin danh sách nhóm thành công!",
                Data = result
            };
        }
    }
}
