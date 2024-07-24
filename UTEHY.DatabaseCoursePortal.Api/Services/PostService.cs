
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Exceptions;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Post;
namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class PostService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly FileService _fileService;
        private readonly UserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly MailService _mailService;
        private readonly TwilioService _twilioService;
        private readonly IMapper _mapper;

        public PostService(ApplicationDbContext dbContext, FileService fileService, IMapper mapper, UserService userService, UserManager<User> userManager, MailService mailService, TwilioService twilioService)
        {
            _dbContext = dbContext;
            _fileService = fileService;
            _mapper = mapper;
            _userService = userService;
            _userManager = userManager;
            _mailService = mailService;
            _twilioService = twilioService;
        }
        public async Task<PagingResult<PostHomeDto>> Get(GetPostRequest request)
        {
            var query = _dbContext.Posts.Where(x => x.DeletedAt == null && x.IsApproved == true).AsQueryable();

            if (!string.IsNullOrEmpty(request.Title))
            {
                string search = request.Title.ToLower();
                query = query.Where(b => b.Title.ToLower().Contains(request.Title.ToLower()));
            }
            if (!string.IsNullOrEmpty(request.Description))
            {
                query = query.Where(b => b.Description.ToLower().Contains(request.Description.ToLower()));
            }

            int total = await query.CountAsync();

            if (request.PageIndex == null) request.PageIndex = 1;
            if (request.PageSize == null) request.PageSize = total;

            int totalPages = (int)Math.Ceiling((double)total / request.PageSize.Value);

            if (string.IsNullOrEmpty(request.OrderBy) && string.IsNullOrEmpty(request.SortBy))
            {
                query = query.OrderByDescending(b => b.Id);
            }
            else if (string.IsNullOrEmpty(request.OrderBy))
            {
                if (request.SortBy == SortByConstant.Asc)
                {
                    query = query.OrderBy(b => b.Id);
                }
                else
                {
                    query = query.OrderByDescending(b => b.Id);
                }
            }
            else if (string.IsNullOrEmpty(request.SortBy))
            {
                query = query.OrderByDescending(b => b.Id);
            }
            else
            {
                if (request.OrderBy == OrderByConstant.Id && request.SortBy == SortByConstant.Asc)
                {
                    query = query.OrderBy(b => b.Id);
                }
                else if (request.OrderBy == OrderByConstant.Id && request.SortBy == SortByConstant.Desc)
                {
                    query = query.OrderByDescending(b => b.Id);
                }
            }

            var items = await query
            .Skip((request.PageIndex.Value - 1) * request.PageSize.Value)
            .Take(request.PageSize.Value)
            .ToListAsync();

            var itemsMapper = _mapper.Map<List<PostHomeDto>>(items);

            var result = new PagingResult<PostHomeDto>(itemsMapper, request.PageIndex.Value, request.PageSize.Value, total, totalPages);

            return result;
        }
        public async Task<PostHomeDto> GetById(int id)
        {
            //var post = await _dbContext.Posts.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == id);

            //var result = _mapper.Map<PostHomeDto>(post);

            //return result;
            var post = await _dbContext.Posts
            .Include(x => x.Comments)
            .Include(x => x.User)  // Thêm dòng này để load thông tin từ bảng Users
            .FirstOrDefaultAsync(x => x.Id == id);

            var result = _mapper.Map<PostHomeDto>(post);

            return result;
        }
        public async Task<PostHomeDto> Delete(int id)
        {
            var post = await _dbContext.Posts.FindAsync(id);

            if (post == null)
            {
                throw new ApiException("Không tìm thấy bài viết có Id hợp lệ!", HttpStatusCode.BadRequest);
            }

            post.DeletedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            //var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == post.UserId);

            //if (user == null)
            //{
            //    throw new ApiException("Không tìm thấy bài viết có Id hợp lệ!", HttpStatusCode.BadRequest);
            //}

            //user.DeletedAt = DateTime.Now;
            var comments = await _dbContext.Comments.Where(x => x.PostId == post.Id).ToListAsync();

            //if (comments == null || comments.Count == 0)
            //{
            //    throw new ApiException("Đã xóa bài viết, bài viết này không có comment!", HttpStatusCode.BadRequest);
            //}
            if (comments != null && comments.Count > 0)
            {
                DateTime now = DateTime.Now;
                foreach (var comment in comments)
                {
                    comment.DeletedAt = now;
                }
            }


            await _dbContext.SaveChangesAsync();


            var result = _mapper.Map<PostHomeDto>(post);

            return result;
        }
        public async Task<PostHomeDto> Edit(EditPostRequest request)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var post = await _dbContext.Posts.FindAsync(request.Id);

                    if (post == null)
                    {
                        throw new ApiException("Không tìm thấy bài viết có Id hợp lệ!", HttpStatusCode.BadRequest);
                    }

                    post.Id = request.Id;

                    await _dbContext.SaveChangesAsync();


                    if (request.ImageFile?.Length > 0)
                    {
                        request.Image = await _fileService.UploadFileAsync(request.ImageFile, PathFolder.Post);
                    }
                    post.Title = request.Title;
                    post.Description = request.Description;
                    post.Image = request.Image;
                    post.Content = request.Content;
                    post.IsPublished=request.IsPublished;

                    await _dbContext.SaveChangesAsync();

                    transaction.Commit();

                    var result = _mapper.Map<PostHomeDto>(post);

                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    throw new ApiException("Có lỗi xảy ra trong quá trình xử lý!", HttpStatusCode.InternalServerError, ex);
                }
            }

        }
        public async Task<PostHomeDto> Create(CreatePostRequest request)
        {

            if (request.ImageFile?.Length > 0)
            {
                request.Image = await _fileService.UploadFileAsync(request.ImageFile, PathFolder.Post);
            }
            var newPost = new Post()
            {
                //Id = request.Id,
                UserId = request.UserId,
                Title = request.Title,
                Description = request.Description,
                Image = request.Image,
                Content = request.Content,
                Priority = request.Priority,
                IsPublished = request.IsPublished,

            };


              _dbContext.Posts.Add(newPost);
              _dbContext.SaveChanges();
            var postHomeDto = _mapper.Map<PostHomeDto>(newPost);

            return postHomeDto;
           
        }
        public async Task<PagingResult<PostHomeDto>> GetApprove(GetPostRequest request)
        {
            var query = _dbContext.Posts.Where(x => x.DeletedAt == null && x.IsApproved == null).AsQueryable();

            if (!string.IsNullOrEmpty(request.Title))
            {
                string search = request.Title.ToLower();
                query = query.Where(b => b.Title.ToLower().Contains(request.Title.ToLower()));
            }
            if (!string.IsNullOrEmpty(request.Description))
            {
                query = query.Where(b => b.Description.ToLower().Contains(request.Description.ToLower()));
            }

            int total = await query.CountAsync();

            if (request.PageIndex == null) request.PageIndex = 1;
            if (request.PageSize == null) request.PageSize = total;

            int totalPages = (int)Math.Ceiling((double)total / request.PageSize.Value);

            if (string.IsNullOrEmpty(request.OrderBy) && string.IsNullOrEmpty(request.SortBy))
            {
                query = query.OrderByDescending(b => b.Id);
            }
            else if (string.IsNullOrEmpty(request.OrderBy))
            {
                if (request.SortBy == SortByConstant.Asc)
                {
                    query = query.OrderBy(b => b.Id);
                }
                else
                {
                    query = query.OrderByDescending(b => b.Id);
                }
            }
            else if (string.IsNullOrEmpty(request.SortBy))
            {
                query = query.OrderByDescending(b => b.Id);
            }
            else
            {
                if (request.OrderBy == OrderByConstant.Id && request.SortBy == SortByConstant.Asc)
                {
                    query = query.OrderBy(b => b.Id);
                }
                else if (request.OrderBy == OrderByConstant.Id && request.SortBy == SortByConstant.Desc)
                {
                    query = query.OrderByDescending(b => b.Id);
                }
            }

            var items = await query
            .Skip((request.PageIndex.Value - 1) * request.PageSize.Value)
            .Take(request.PageSize.Value)
            .ToListAsync();

            var itemsMapper = _mapper.Map<List<PostHomeDto>>(items);

            var result = new PagingResult<PostHomeDto>(itemsMapper, request.PageIndex.Value, request.PageSize.Value, total, totalPages);

            return result;
        }

        public async Task<PostHomeDto> Approve(ApprovePostRequest request)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var post = await _dbContext.Posts.FindAsync(request.Id);

                    if (post == null)
                    {
                        throw new ApiException("Không tìm thấy bài viết có Id hợp lệ!", HttpStatusCode.BadRequest);
                    }

                    post.Id = request.Id;

                    await _dbContext.SaveChangesAsync();



                    post.IsApproved = request.IsApproved;

                    await _dbContext.SaveChangesAsync();

                    transaction.Commit();

                    var result = _mapper.Map<PostHomeDto>(post);

                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    throw new ApiException("Có lỗi xảy ra trong quá trình xử lý!", HttpStatusCode.InternalServerError, ex);
                }
            }

        }
     

        public async Task<ApproveMultipleCommentResult<int>> ApproveMultiple(List<ApprovePostRequest> requests)
        {
            var successfulIds = new List<int>();
            var failedIds = new List<int>();

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var request in requests)
                    {
                        var post = await _dbContext.Posts.FindAsync(request.Id);

                        if (post == null)
                        {
                            failedIds.Add(request.Id ?? 0);
                            continue;
                        }

                        // Cập nhật các trường dữ liệu cần thiết tương ứng với yêu cầu của bạn
                        post.IsApproved = request.IsApproved;


                        await _dbContext.SaveChangesAsync();

                        successfulIds.Add(request.Id ?? 0);
                    }

                    transaction.Commit();

                    var result = new ApproveMultipleCommentResult<int>
                    {
                        SuccessfulItems = successfulIds,
                        FailedItems = failedIds
                    };

                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    // Xử lý lỗi và ném ra ApiException nếu cần thiết
                    throw new ApiException("Có lỗi xảy ra trong quá trình xử lý!", HttpStatusCode.InternalServerError, ex);
                }
            }
        }






    }
}
