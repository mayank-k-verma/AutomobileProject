using Automobile.DataService.Data;
using Automobile.DataService.Repositories.Interfaces;
using Automobile.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace Automobile.DataService.Repositories;

public class DriverRepository : GenericRepository<Driver>, IDriverRepository
{
    public DriverRepository(AppDbContext dbContext) : base(dbContext) {  }

    public override async Task<IEnumerable<Driver>> All()
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

    public override async Task<bool> Update(Driver driver)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == driver.Id);

            if(result == null)
                return false;
            
            result.UpdatedDate = DateTime.UtcNow;
            result.DriverNumber  = driver.DriverNumber;
            result.Name = driver.Name;
            result.DateOfBirth = driver.DateOfBirth;
            
            return true;
        }
        catch (System.Exception)
        {
            Console.WriteLine("Update function error!");
            throw;
        }
    }
}