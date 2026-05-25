namespace Assignment01.Interfaces;

using Assignment01.Models;

public interface IUserInterface
{
    void Add(User user);
    User? GetByID(string userId);
    IEnumerable<User> GetAll();
    bool Exists(string userId);
}
