using Homework01.Core.Enums;
using Homework01.Core.Models;
using IServiceProvider = Homework01.Core.Interfaces.IServiceProvider;

namespace Homework01.Core;

public class ServiceProvider(IReadOnlyList<ServiceDescriptor> services)
    : IServiceProvider,
        IDisposable
{
    private readonly IReadOnlyList<ServiceDescriptor> _services = services;
    private readonly Dictionary<Type, object> _singletonInstances = [];
    private readonly Dictionary<Type, object> _scopedInstances = [];

    public void Dispose() { }

    public object GetService(Type serviceType)
    {
        var descriptor =
            _services.FirstOrDefault(dsc => dsc.ServiceType == serviceType)
            ?? throw new Exception($"Service {serviceType.Name} not registered.");

        switch (descriptor.Lifetime)
        {
            case ServiceLifetime.Transient:
                return CreateInstance(descriptor);
            case ServiceLifetime.Scoped:
                if (_scopedInstances.TryGetValue(serviceType, out var scopedInstance))
                {
                    return scopedInstance;
                }
                var newScopedInstance = CreateInstance(descriptor);
                _scopedInstances[serviceType] = newScopedInstance;
                return newScopedInstance;
            case ServiceLifetime.Singleton:
                if (_singletonInstances.TryGetValue(serviceType, out var instance))
                {
                    return instance;
                }
                var newInstance = CreateInstance(descriptor);
                _singletonInstances[serviceType] = newInstance;
                return newInstance;
            default:
                throw new Exception("Unknown Lifetime");
        }

        throw new NotImplementedException();
    }

    private object CreateInstance(ServiceDescriptor descriptor)
    {
        var constructor = descriptor.ImplementationType.GetConstructors().First();

        var parameters = constructor
            .GetParameters()
            .Select(p => GetService(p.ParameterType))
            .ToArray();

        return Activator.CreateInstance(descriptor.ImplementationType, parameters)
            ?? throw new Exception(
                $"Failed to create instance of {descriptor.ImplementationType.Name}."
            );
    }

    public TService GetService<TService>() => (TService)GetService(typeof(TService));

    public ServiceProvider CreateScope() => new(_services);
}
