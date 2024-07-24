using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Constants;
using UTEHY.DatabaseCoursePortal.Api.Data.Entities;
using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;
using UTEHY.DatabaseCoursePortal.Api.Exceptions;

namespace UTEHY.DatabaseCoursePortal.Api.Services
{
    public class ConfigService
    {
        private readonly ApplicationDbContext _dbContext;

        public ConfigService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetConfigValue(string key)
        {
            var value = await _dbContext.Configs.Where(x => x.Key == key).FirstOrDefaultAsync();

            return value.Value;
        }

        public async Task<bool> UpdateConfigValue(string key, string newValue)
        {
            var configEntity = await _dbContext.Configs
                .FirstOrDefaultAsync(x => x.Key == key);

            if (configEntity == null)
            {
                throw new ApiException("Không thêm thấy config với key đầu vào!", HttpStatusCode.BadRequest);
            }

            configEntity.Value = newValue;

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
