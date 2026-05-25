namespace Assignment01.Interfaces;

public interface IBaseRepository<T>
{
    void Add(T t);
    T? GetById(string id);
    IEnumerable<T> GetAll();
    bool Exists(string id);
}
