using System.Data.Common;
using Automobile.DataService.Data;
using Automobile.DataService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Automobile.DataService.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{

    protected AppDbContext _dbContext;
    internal DbSet<T> _dbSet;

    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }

    public virtual async Task<bool> Add(T entity)
    {
        await _dbSet.AddAsync(entity);
        //save changes is not required
        return true;
        
    }

    public virtual Task<IEnumerable<T>> All()
    {
        throw new NotImplementedException();
    }

    public virtual Task<bool> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<T?> GetById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual Task<bool> Update(T entity)
    {
        throw new NotImplementedException();
    }
}