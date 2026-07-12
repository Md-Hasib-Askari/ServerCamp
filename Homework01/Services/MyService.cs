using Homework01.Interfaces;

namespace Homework01.Services;

public class MyService : IMyService
{
    private readonly Guid _id = Guid.NewGuid();

    public Guid GetGuid() => _id;
}
