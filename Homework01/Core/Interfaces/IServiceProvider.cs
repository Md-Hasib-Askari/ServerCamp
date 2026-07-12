using Homework01.Core;

namespace Homework01.Core.Interfaces;

public interface IServiceProvider
{
    TService GetService<TService>();
    ServiceProvider CreateScope();
}
