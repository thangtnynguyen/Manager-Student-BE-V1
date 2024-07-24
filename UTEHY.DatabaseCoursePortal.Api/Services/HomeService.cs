using AutoMapper;
using Google.Apis.YouTube.v3.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Models.Banner;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Course;
using UTEHY.DatabaseCoursePortal.Api.Models.GoogleClould;
using UTEHY.DatabaseCoursePortal.Api.Models.Home;
using UTEHY.DatabaseCoursePortal.Api.Models.Page;
using UTEHY.DatabaseCoursePortal.Api.Models.Post;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class HomeService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ConfigService _configService;
        private readonly GoogleClouldService _googleClouldService;
        private readonly IMapper _mapper;

        public HomeService(ApplicationDbContext dbContext, ConfigService configService, IMapper mapper, GoogleClouldService googleClouldService)
        {
            _dbContext = dbContext;
            _configService = configService;
            _mapper = mapper;
            _googleClouldService = googleClouldService;
        }

        public async Task<List<BannerDto>> GetBanners(Models.Home.GetBannerRequest request)
        {
            var maxBannerHomeConfig = await _configService.GetConfigValue(ConfigConstant.MaxCourseHome);
            var query = _dbContext.Banners.AsQueryable();

            var banners = await query.Where(b => b.Type == request.Type && b.Place == request.Place && b.Expired > DateTime.UtcNow)
            .OrderByDescending(b => b.Priority).ToListAsync();

            var result = _mapper.Map<List<BannerDto>>(banners);

            return result;
        }

        public async Task<List<CourseHomeDto>> GetCourses()
        {
            var maxCourseBannerConfig = await _configService.GetConfigValue(ConfigConstant.MaxCourseHome);
            var maxCourseHome = int.Parse(maxCourseBannerConfig);

            var query = _dbContext.Courses.Include(course => course.UserCourses).AsQueryable();

            var courses = await query
            .Take(maxCourseHome)
            .ToListAsync();

            var result = _mapper.Map<List<CourseHomeDto>>(courses);

            return result;
        }

        public async Task<List<PageHomeDto>> GetFeaturePages()
        {
            var maxPageFeatureConfig = await _configService.GetConfigValue(ConfigConstant.MaxPageFeatureHome);
            var maxPageFeature = int.Parse(maxPageFeatureConfig);

            var query = _dbContext.Pages.AsQueryable();

            var pages = await query
            .Where(b => b.Status == BooleanConstant.True && b.Type == TypePageConstant.Feature)
            .OrderByDescending(b => b.Priority)
            .Take(maxPageFeature)
            .ToListAsync();

            var result = _mapper.Map<List<PageHomeDto>>(pages);

            return result;
        }

        public async Task<List<PostHomeDto>> GetPosts()
        {
            var maxPostConfig = await _configService.GetConfigValue(ConfigConstant.MaxPostHome);
            var maxPost = int.Parse(maxPostConfig);

            var query = _dbContext.Posts.Include(post => post.User).AsQueryable();

            var posts = await query
            .Where(b => b.IsPublished == BooleanConstant.True && b.IsApproved == true)
            .OrderByDescending(b => b.Priority)
            .Take(maxPost)
            .ToListAsync();

            var result = _mapper.Map<List<PostHomeDto>>(posts);

            return result;
        }

        public async Task<List<VideoYoutube>> GetVideos()
        {
            var idChannel = await _configService.GetConfigValue(ConfigConstant.IdChannelYoutube);

            var maxVideoYoutubeHomeConfig = await _configService.GetConfigValue(ConfigConstant.MaxVideoYoutubeHome);
            var maxVideoYoutubeHome = int.Parse(maxVideoYoutubeHomeConfig);

            var result = await _googleClouldService.GetListInfoVideoByChannel(idChannel, maxVideoYoutubeHome);

            return result;
        }
    }
}
