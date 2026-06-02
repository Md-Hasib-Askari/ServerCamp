namespace Assignment01.Models;

public class User
{
    public string UserId { get; private set; }
    public string FullName { get; private set; }
    public string MobileNumber { get; private set; }
    public string Email { get; private set; }

    public User(string userId, string fullName, string mobileNumber, string email)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required.");
        if (string.IsNullOrWhiteSpace(mobileNumber))
            throw new ArgumentException("Mobile number is required.");
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");

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
