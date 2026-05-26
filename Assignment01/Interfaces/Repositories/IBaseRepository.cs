namespace Assignment01.Interfaces;

public interface IBaseRepository<T>
{
    public void Add(T t);
    public T? GetById(string id);
    public IEnumerable<T> GetAll();
    public bool Exists(string id);
}
