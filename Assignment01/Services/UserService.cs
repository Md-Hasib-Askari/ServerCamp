using Assignment01.Interfaces;
using Assignment01.Interfaces.Services;
using Assignment01.Models;

namespace Assignment01.Services;

public class UserService : IUserService
{
    private readonly IBaseRepository<User> _userRepo;
    private readonly IIdGenerator _idGenerator;

    public UserService(IBaseRepository<User> userRepo, IIdGenerator idGenerator)
    {
        _userRepo = userRepo;
        _idGenerator = idGenerator;
    }

    public User CreateUser(string fullname, string mobileNumber, string email)
    {
        // Validate first so a rejected user never consumes an ID.
        User.Validate(fullname, mobileNumber, email);
        var user = new User(_idGenerator.GenerateUserId(), fullname, mobileNumber, email);
        _userRepo.Add(user);
        return user;
    }

    public IEnumerable<User> GetAllUsers() => _userRepo.GetAll();

    public User? GetUserById(string userId) => _userRepo.GetById(userId);
}
