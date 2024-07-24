using UTEHY.DatabaseCoursePortal.Api.Data.EntityFrameworkCore;

namespace UTEHY.DatabaseCoursePortal.Api.Providers;

public class DeleteEntity
{
    private readonly ApplicationDbContext _dbContext;

    public DeleteEntity(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Delete(Type type, int id)
    {
        var entity = _dbContext.Find(type, id);
        if (entity != null)
        {

        }
    }
}