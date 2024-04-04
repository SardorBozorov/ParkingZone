using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using ParkingZone.Domain.Commons;
using System.Linq.Expressions;

namespace ParkingZone.Data.IRepositories;

public interface  IRepository<T> where T : Auditable
{
    T Create (T entity);
    T Update (T entity);
    bool Delete (Expression<Func<T, bool>> expression);
    T Get(Expression<Func<T, bool>> expression);
    IQueryable<T> GetAll(Expression<Func<T,bool>>? expression = null);
}
