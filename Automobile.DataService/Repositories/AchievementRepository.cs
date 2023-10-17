using Automobile.DataService.Data;
using Automobile.DataService.Repositories.Interfaces;
using Automobile.Entities.DbSet;
using Microsoft.EntityFrameworkCore;

namespace Automobile.DataService.Repositories;

public class AchievementRepository : GenericRepository<Achievement>, IAchievementRepository
{
    public AchievementRepository(AppDbContext dbContext) : base(dbContext) {  }

    public async Task<Achievement?> GetDriverAchievementAsync(Guid driverId)
    {
        try
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.DriverId == driverId);
        }
        catch (System.Exception)
        {
            Console.WriteLine("GetDriverAchievementAsync function error!");
            throw;
        }
    }

    public override async Task<IEnumerable<Achievement>> All()
    {
        try
        {
            return await _dbSet.Where(x => x.Status == 1)
            .AsNoTracking()
            .AsSplitQuery()
            .OrderBy(x => x.AddedDate)
            .ToListAsync();
        }
        catch (System.Exception)
        {
            Console.WriteLine("Delete function error!");
            throw;
        }
    }

    public override async Task<bool> Delete(Guid id)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

           if(result == null)
                return false;
            
            result.Status = 0;
            result.UpdatedDate = DateTime.UtcNow;
            return true;
        }
        catch (System.Exception)
        {
            Console.WriteLine("Delete function error!");
            throw;
        }
    }

    public override async Task<bool> Update(Achievement achievement)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == achievement.Id);

            if(result == null)
                return false;
            
            result.UpdatedDate = DateTime.UtcNow;
            result.WorldChampionship  = achievement.WorldChampionship;
            result.FastestLap = achievement.FastestLap;
            result.RaceWins = achievement.RaceWins;
            result.PolePosition = achievement.PolePosition;
            
            return true;
        }
        catch (System.Exception)
        {
            Console.WriteLine("Update function error!");
            throw;
        }
    }
}