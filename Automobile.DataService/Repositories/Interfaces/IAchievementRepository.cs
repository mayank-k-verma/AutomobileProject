using Automobile.Entities.DbSet;

namespace Automobile.DataService.Repositories.Interfaces;

public interface IAchievementRepository : IGenericRepository<Achievement>{
    public Task<Achievement?> GetDriverAchievementAsync(Guid driverId);
}