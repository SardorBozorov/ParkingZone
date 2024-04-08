using Microsoft.EntityFrameworkCore;
using Parking_Zone.Data.DbCondext;
using Parking_Zone.Data.IRepositories;
using Parking_Zone.Domain.Commons;
using System.Linq.Expressions;

namespace Parking_Zone.Data.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<T> _dbSet;
    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }
    public void Create(T entity)
    {
        _dbSet.Add(entity);
        _dbContext.SaveChanges();
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

    public IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null)
    {
        var query = expression is null ? _dbSet : _dbSet.Where(expression);
        return query;
    }

    public void Update(T entity)
    {
        _dbContext.Update(entity);

        _dbContext.SaveChanges();
    }
}
