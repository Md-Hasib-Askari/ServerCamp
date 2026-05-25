namespace Assignment01.Interfaces.Services;

using Assignment01.Models;

public interface IUserService
{
    User CreateUser(string fullname, string mobileNumber, string email);
    IEnumerable<User> GetAllUsers();
    User? GetUserById(string userId);
}
