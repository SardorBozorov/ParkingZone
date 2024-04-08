using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Parking_Zone.Domain.Commons;
using System.Linq.Expressions;

namespace Parking_Zone.Data.IRepositories;

public interface  IRepository<T> where T : class
{
    void Create (T entity);
    void Update (T entity);
    bool Delete (Expression<Func<T, bool>> expression);
    T Get(Expression<Func<T, bool>> expression);
    IQueryable<T> GetAll(Expression<Func<T,bool>>? expression = null);
}
