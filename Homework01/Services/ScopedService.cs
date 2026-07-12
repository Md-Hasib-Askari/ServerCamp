using Homework01.Interfaces;

namespace Homework01.Services
{
    public class ScopedService : IScopedService
    {
        private readonly Guid _guidId = Guid.NewGuid();

        public Guid GetGuid() => _guidId;
    }
}
