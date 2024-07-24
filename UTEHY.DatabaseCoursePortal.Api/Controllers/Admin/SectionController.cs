using Microsoft.AspNetCore.Mvc;
using UTEHY.DatabaseCoursePortal.Api.Models.Common;
using UTEHY.DatabaseCoursePortal.Api.Models.Section;
using UTEHY.DatabaseCoursePortal.Api.Services;

namespace UTEHY.DatabaseCoursePortal.Api.Controllers.Admin
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly SectionService _sectionService;

        public SectionController(SectionService sectionService)
        {
            _sectionService = sectionService;
        }

        [HttpGet("get-all")]
        public async Task<ApiResult<List<Data.Entities.Section>>> GetAll()
        {
            try
            {
                var result = await _sectionService.GetAll();

                return new ApiResult<List<Data.Entities.Section>>()
                {
                    Status = true,
                    Message = "Lấy danh sách chương thành công!",
                    Data = result
                };
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("get-section-with-lesson")]
        public async Task<ApiResult<Data.Entities.Section>> GetSectionWithLesson([FromQuery] int id)
        {
            try
            {
                var result = await _sectionService.GetSectionWithLesson(id);

                return new ApiResult<Data.Entities.Section>()
                {
                    Status = true,
                    Message = "Lấy thông tin chương thành công!",
                    Data = result
                };
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            catch (BadHttpRequestException ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<ApiResult<Data.Entities.Section>> Create(CreateSectionRequest request)
        {
            try
            {
                var result = await _sectionService.Create(request);

                return new ApiResult<Data.Entities.Section>()
                {
                    Status = true,
                    Message = "Tạo mới chương thành công!",
                    Data = result
                };
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("edit")]
        public async Task<ApiResult<Data.Entities.Section>> Edit(EditSectionRequest request)
        {
            try
            {
                var result = await _sectionService.Edit(request);

                return new ApiResult<Data.Entities.Section>()
                {
                    Status = true,
                    Message = "Cập nhật chương thành công!",
                    Data = result
                };
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("delete")]
        public async Task<ApiResult<Data.Entities.Section>> Delete(EntityIdentityRequest<int> request)
        {
            var result = await _sectionService.Delete(request.Id);

            return new ApiResult<Data.Entities.Section>()
            {
                Status = true,
                Message = "Xoá thành công chương!",
                Data = result
            };
        }
    }
}
