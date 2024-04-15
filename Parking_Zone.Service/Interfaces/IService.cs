namespace Parking_Zone.Service.Interfaces;

public interface IService<T> where T : class
{
    T GetById(long? id);

    IEnumerable<T> GetAll();

    void Create(T entity);

    void Update(T entity);

    void Delete(T entity);
}
