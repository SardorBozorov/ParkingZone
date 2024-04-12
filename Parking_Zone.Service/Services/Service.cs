using Parking_Zone.Data.IRepositories;
using Parking_Zone.Service.Interfaces;

namespace Parking_Zone.Service.Services;

public class Service<T> : IService<T> where T : class
{
    private readonly IRepository<T> _repository;
    public Service(IRepository<T> repository) 
    { 
        _repository = repository; 
    }
    public T GetById(long? id)
    {
        return _repository.GetById(id);
    }

    public virtual void Create(T entity)
    {
        _repository.Create(entity);
    }

    public void Delete(T entitiy)
    {
        _repository.Delete(entitiy);
    }

    public IEnumerable<T> GetAll()
    {
        return _repository.GetAll();
    }

    public void Update(T entity)
    {
       _repository.Update(entity);
    }
}
