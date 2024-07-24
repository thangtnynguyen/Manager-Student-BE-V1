using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Banner;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly BannerService _bannerService;

        public BannerController(BannerService bannerService)
        {
            _bannerService = bannerService;
        }

        [HttpGet("get")]
        public async Task<ApiResult<PagingResult<Banner>>> Get([FromQuery] GetBannerRequest request)
        {
            var result = await _bannerService.Get(request);

            return new ApiResult<PagingResult<Banner>>()
            {
                Status = true,
                Message = "Danh sách banner đã được lấy thành công!",
                Data = result
            };
        }

        [HttpGet("get-by-id")]
        public async Task<ApiResult<Banner>> GetById([FromQuery] int id)
        {
            var result = await _bannerService.GetById(id);

            return new ApiResult<Banner>()
            {
                Status = true,
                Message = "Danh sách banner đã được lấy thành công!",
                Data = result
            };
        }

        [HttpPost("create")]
        public async Task<ApiResult<Banner>> Create([FromForm] CreateBannerRequest request)
        {
            var result = await _bannerService.Create(request);

            return new ApiResult<Banner>()
            {
                Status = true,
                Message = "Danh sách banner đã được lấy thành công!",
                Data = result
            };
        }
    }
}
