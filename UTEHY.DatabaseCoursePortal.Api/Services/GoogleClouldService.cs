using AutoMapper;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Channels;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Helpers;
using UTEHY.DatabaseCoursePortal.Api.Models.Course;
using UTEHY.DatabaseCoursePortal.Api.Models.GoogleClould;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class GoogleClouldService
    {
        private readonly IConfiguration _config;
        private readonly YouTubeService _youtubeService;
        private readonly IMapper _mapper;

        public GoogleClouldService(IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
            _youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _config["GoogleClould:ApiKey"],
                ApplicationName = _config["GoogleClould:ApplicationName"]
            });
        }

        #region Youtube

        public async Task<VideoYoutube> GetInfoVideoYoutube(string videoId)
        {
            var videoRequest = _youtubeService.Videos.List("snippet,statistics");

            videoRequest.Id = videoId;

            var videoResponse = await videoRequest.ExecuteAsync();

            var video = videoResponse.Items[0];

            var result = _mapper.Map<VideoYoutube>(video);

            return result;
        }

        public async Task<List<VideoYoutube>> GetListInfoVideoByChannel(string idChannel,int maxNumberVideo)
        {

            //Get video ids
            var searchListRequest = _youtubeService.Search.List("snippet");
            searchListRequest.ChannelId = idChannel;
            searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Date;
            searchListRequest.Type = GoogleClouldConstant.TypeResourceVideo;
            searchListRequest.MaxResults = maxNumberVideo;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            var videoIds = searchListResponse.Items.Select(item => item.Id.VideoId).ToList();
            
            //Get info videos
            var videoRequest = _youtubeService.Videos.List("snippet,statistics,contentDetails");
            videoRequest.Id = string.Join(",", videoIds);

            var videoResponse = await videoRequest.ExecuteAsync();

            var videos = videoResponse.Items.Select(video => new VideoYoutube
            {
                Id = video.Id,
                UrlVideo = GoogleClouldConstant.BaseUrlVideo + video.Id,
                Title = video.Snippet.Title,
                ThumbnailUrl = video.Snippet.Thumbnails.Default__.Url,
                LikeCount = video.Statistics.LikeCount,
                CommentCount = video.Statistics.CommentCount,
                ViewCount = video.Statistics.ViewCount,
                Duration = DateTimeHelper.ConvertIso8601ToHoursMinutesSeconds(video.ContentDetails.Duration)
            }).ToList();

            return videos;
        }
        #endregion
    }
}
