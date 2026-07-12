using Homework01.Core.Enums;
using Homework01.Core.Interfaces;
using Homework01.Core.Models;

namespace Homework01.Core;

public class ServiceCollection : IServiceCollection
{
    private readonly List<ServiceDescriptor> _services = [];

    public void AddScoped<TService, TImplementation>()
        where TImplementation : TService
    {
        _services.Add(
            new ServiceDescriptor(
                serviceType: typeof(TService),
                implementationType: typeof(TImplementation),
                lifetime: ServiceLifetime.Scoped
            )
        );
    }

    public void AddSingleton<TService, TImplementation>()
        where TImplementation : TService
    {
        _services.Add(
            new ServiceDescriptor(
                serviceType: typeof(TService),
                implementationType: typeof(TImplementation),
                lifetime: ServiceLifetime.Singleton
            )
        );
    }

    public void AddTransient<TService, TImplementation>()
        where TImplementation : TService
    {
        _services.Add(
            new ServiceDescriptor(
                serviceType: typeof(TService),
                implementationType: typeof(TImplementation),
                lifetime: ServiceLifetime.Transient
            )
        );
    }

    public ServiceProvider BuildServiceProvider() => new(_services.AsReadOnly());
}
