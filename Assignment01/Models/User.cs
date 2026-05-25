namespace Assignment01.Models;

public class User
{
    public string UserId { get; private set; }
    public string FullName { get; private set; }
    public string MobileNumber { get; private set; }
    public string Email { get; private set; }

    public User(string userId, string fullName, string mobileNumber, string email)
    {
        UserId = userId;
        FullName = fullName;
        MobileNumber = mobileNumber;
        Email = email;
    }

    public override string ToString()
    {
        return $"[{UserId}] {FullName} | {MobileNumber} | {Email}";
    }
}
