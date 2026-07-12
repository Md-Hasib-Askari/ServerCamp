using Homework01.Core;
using Homework01.Interfaces;
using Homework01.Services;

var services = new ServiceCollection();

services.AddTransient<IMyService, MyService>();

// services.AddSingleton<IMyService, MyService>(); // duplicate registration for demonstration purposes
services.AddSingleton<IEmailService, EmailService>();
services.AddScoped<IScopedService, ScopedService>();

var provider = services.BuildServiceProvider();

var transientSvc1 = provider.GetService<IMyService>();
var transientSvc2 = provider.GetService<IMyService>();

// var s1 = provider.GetService<IMyService>();
// var s2 = provider.GetService<IMyService>();

var singletonSvc1 = provider.GetService<IEmailService>();
var singletonSvc2 = provider.GetService<IEmailService>();

using (var scope = provider.CreateScope())
{
    var scopedSvc1 = scope.GetService<IScopedService>();
    var scopedSvc2 = scope.GetService<IScopedService>();

    Console.WriteLine("Scoped Example:");
    Console.WriteLine(scopedSvc1.GetGuid());
    Console.WriteLine(scopedSvc2.GetGuid());
}

Console.WriteLine();

using (var scope = provider.CreateScope())
{
    var scopedSvc1 = scope.GetService<IScopedService>();
    var scopedSvc2 = scope.GetService<IScopedService>();

    Console.WriteLine("Scoped Example:");
    Console.WriteLine(scopedSvc1.GetGuid());
    Console.WriteLine(scopedSvc2.GetGuid());
}

Console.WriteLine();

Console.WriteLine("Transient Example:");
Console.WriteLine(transientSvc1.GetGuid());
Console.WriteLine(transientSvc2.GetGuid());

Console.WriteLine();

Console.WriteLine("Singleton Example:");
Console.WriteLine(singletonSvc1.GetGuid());
Console.WriteLine(singletonSvc2.GetGuid());

// Console.WriteLine();
//
// Console.WriteLine("Singleton Example (duplicate):");
// Console.WriteLine(s1.GetGuid());
// Console.WriteLine(s2.GetGuid());
//
// Console.WriteLine();
