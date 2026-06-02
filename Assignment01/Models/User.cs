namespace Assignment01.Models;

public class User
{
    public string UserId { get; private set; }
    public string FullName { get; private set; }
    public string MobileNumber { get; private set; }
    public string Email { get; private set; }

    public User(string userId, string fullName, string mobileNumber, string email)
    {
        Validate(fullName, mobileNumber, email);

        UserId = userId;
        FullName = fullName;
        MobileNumber = mobileNumber;
        Email = email;
    }

    // Lets a caller check the inputs before generating an ID, so a rejected user wastes no ID.
    public static void Validate(string fullName, string mobileNumber, string email)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name is required.");
        if (string.IsNullOrWhiteSpace(mobileNumber))
            throw new ArgumentException("Mobile number is required.");
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required.");
    }

    public override string ToString()
    {
        return $"[{UserId}] {FullName} | {MobileNumber} | {Email}";
    }
}
