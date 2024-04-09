using System.Linq.Expressions;

namespace Parking_Zone.Data.IRepositories;

public interface IRepository<T> where T : class
{
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);
    T GetById(long? id);
    IEnumerable<T> GetAll();
}
