using Homework01.Interfaces;

namespace Homework01.Services;

public class EmailService : IEmailService
{
    private readonly Guid _id = Guid.NewGuid();

    public Guid GetGuid() => _id;
}
