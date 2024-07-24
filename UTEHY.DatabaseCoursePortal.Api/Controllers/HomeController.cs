using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Models.Banner;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Course;
using UTEHY.DatabaseCoursePortal.Api.Models.GoogleClould;
using UTEHY.DatabaseCoursePortal.Api.Models.Home;
using UTEHY.DatabaseCoursePortal.Api.Models.Page;
using UTEHY.DatabaseCoursePortal.Api.Models.Post;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers
{
    [Route("api/[controller]")]
    public class HomeController
    {
        private readonly HomeService _homeService;

        public HomeController(HomeService homeService)
        {
            _homeService = homeService;
        }

        [HttpGet("get-banners")]
        public async Task<ApiResult<List<BannerDto>>> GetBanners(Models.Home.GetBannerRequest request)
        {
            var result = await _homeService.GetBanners(request);

            return new ApiResult<List<BannerDto>>()
            {
                Status = true,
                Message = "Danh sách banner đã được lấy thành công!",
                Data = result
            };
        }

        [HttpGet("get-courses")]
        public async Task<ApiResult<List<CourseHomeDto>>> GetCourses()
        {
            var result = await _homeService.GetCourses();

            return new ApiResult<List<CourseHomeDto>>()
            {
                Status = true,
                Message = "Danh sách khoá học thành công!",
                Data = result
            };
        }

        [HttpGet("get-feature-pages")]
        public async Task<ApiResult<List<PageHomeDto>>> GetFeaturePages()
        {
            var result = await _homeService.GetFeaturePages();

            return new ApiResult<List<PageHomeDto>>()
            {
                Status = true,
                Message = "Danh sách trang tính năng thành công!",
                Data = result
            };
        }

        [HttpGet("get-posts")]
        public async Task<ApiResult<List<PostHomeDto>>> GetPosts()
        {
            var result = await _homeService.GetPosts();

            return new ApiResult<List<PostHomeDto>>()
            {
                Status = true,
                Message = "Lấy danh sách bài viết thành công!",
                Data = result
            };
        }

        [HttpGet("get-videos")]
        public async Task<ApiResult<List<VideoYoutube>>> GetVideos()
        {
            var result = await _homeService.GetVideos();

            return new ApiResult<List<VideoYoutube>>()
            {
                Status = true,
                Message = "Lấy danh sách video youtube trang chủ thành công!",
                Data = result
            };
        }
    }
}
