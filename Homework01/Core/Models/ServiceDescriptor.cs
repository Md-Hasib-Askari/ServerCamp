using Homework01.Core.Enums;

namespace Homework01.Core.Models;

public class ServiceDescriptor(Type serviceType, Type implementationType, ServiceLifetime lifetime)
{
    public Type ServiceType { get; } = serviceType;
    public Type ImplementationType { get; } = implementationType;
    public ServiceLifetime Lifetime { get; } = lifetime;
}
