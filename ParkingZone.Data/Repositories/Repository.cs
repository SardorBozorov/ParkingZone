using Microsoft.EntityFrameworkCore;
using Parking_Zone.Data.DbCondext;
using Parking_Zone.Data.IRepositories;

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

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
        _dbContext.SaveChanges();
    }

    public T GetById(long? id)
    {
        return _dbSet.Find(id);
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet;
    }

    public void Update(T entity)
    {
        _dbContext.Update(entity);
        _dbContext.SaveChanges();
    }
}
