namespace Homework01.Core.Interfaces;

public interface IServiceCollection
{
    void AddTransient<TService, TImplementation>()
        where TImplementation : TService;
    void AddScoped<TService, TImplementation>()
        where TImplementation : TService;
    void AddSingleton<TService, TImplementation>()
        where TImplementation : TService;
    ServiceProvider BuildServiceProvider();
}
