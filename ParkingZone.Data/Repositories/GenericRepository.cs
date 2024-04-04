using Microsoft.EntityFrameworkCore;
using ParkingZone.Data.DbCondext;
using ParkingZone.Data.IRepositories;
using ParkingZone.Domain.Commons;
using System.Linq.Expressions;

namespace ParkingZone.Data.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : Auditable
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<T> _dbSet;
    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }
    public T Create(T entity)
    {
        var result = _dbSet.Add(entity);
        _dbContext.SaveChanges();
        return result.Entity;
    }

    public bool Delete(Expression<Func<T, bool>> expression)
    {
        // Find entities that match the deletion condition.
        var entitiesToDelete =  _dbSet.FirstOrDefault(expression);

        // If there are entities to delete, remove them from the DbSet.
        if (entitiesToDelete != null)
        {
            _dbSet.RemoveRange(entitiesToDelete);

            // Save changes to the database.
            _dbContext.SaveChangesAsync();
        }

        return true;
    }

    public T Get(Expression<Func<T, bool>> expression)
    {
        var result = _dbSet.FirstOrDefault(expression);
        return result;
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>>? expression = null)
    {
        var query = expression is null ? _dbSet : _dbSet.Where(expression);
        return query;
    }

    public T Update(T entity)
    {
        var entry = _dbContext.Update(entity);

        _dbContext.SaveChanges();

        return entry.Entity;
    }
}
