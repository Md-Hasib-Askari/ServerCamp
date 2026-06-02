using Assignment01.Interfaces;
using Assignment01.Models;

namespace Assignment01.Services;

public class UserRepository : IBaseRepository<User>
{
    private readonly Dictionary<string, User> _users = new();

    public void Add(User user) => _users[user.UserId] = user;

    public bool Exists(string userId) => _users.ContainsKey(userId);

    public IEnumerable<User> GetAll() => _users.Values;

    public User? GetById(string userId) => _users.TryGetValue(userId, out var u) ? u : null;
}
