namespace Assignment01.Services;

using Assignment01.Interfaces;
using Assignment01.Models;

public class UserRepository : IBaseRepository<User>
{
    public void Add(User t)
    {
        throw new NotImplementedException();
    }

    public bool Exists(string id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetAll()
    {
        throw new NotImplementedException();
    }

    public User? GetById(string id)
    {
        throw new NotImplementedException();
    }
}
