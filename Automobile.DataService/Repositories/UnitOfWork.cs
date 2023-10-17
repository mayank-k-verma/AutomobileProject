using Automobile.DataService.Data;
using Automobile.DataService.Repositories.Interfaces;

namespace Automobile.DataService.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    
    protected AppDbContext _dbContext;
    public IDriverRepository Drivers { get; }
    public IAchievementRepository Achievements { get; }
    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        Drivers = new DriverRepository(_dbContext);
        Achievements = new AchievementRepository(_dbContext);
    }

    

    public async Task<bool> CompleteAsync()
    {
        var result = await _dbContext.SaveChangesAsync();
        return result > 0;
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}