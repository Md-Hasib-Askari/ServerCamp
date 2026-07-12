using Homework01.Core;
using Homework01.Interfaces;
using Homework01.Services;

var services = new ServiceCollection();

services.AddTransient<IMyService, MyService>();
services.AddSingleton<IMyService, MyService>(); // duplicate registration for demonstration purposes
services.AddSingleton<IEmailService, EmailService>();
services.AddScoped<IScopedService, ScopedService>();

var provider = services.BuildServiceProvider();

var a = provider.GetService<IMyService>();
var b = provider.GetService<IMyService>();

var s1 = provider.GetService<IMyService>();
var s2 = provider.GetService<IMyService>();

var e1 = provider.GetService<IEmailService>();
var e2 = provider.GetService<IEmailService>();

using (var scope = provider.CreateScope())
{
    var scopedService1 = scope.GetService<IScopedService>();
    var scopedService2 = scope.GetService<IScopedService>();

    Console.WriteLine("Scoped Example:");
    Console.WriteLine(scopedService1.GetGuid());
    Console.WriteLine(scopedService2.GetGuid());
}

Console.WriteLine();

using (var scope = provider.CreateScope())
{
    var scopedService1 = scope.GetService<IScopedService>();
    var scopedService2 = scope.GetService<IScopedService>();

    Console.WriteLine("Scoped Example:");
    Console.WriteLine(scopedService1.GetGuid());
    Console.WriteLine(scopedService2.GetGuid());
}

Console.WriteLine();

Console.WriteLine("Transient Example:");
Console.WriteLine(a.GetGuid());
Console.WriteLine(b.GetGuid());

Console.WriteLine();

Console.WriteLine("Singleton Example:");
Console.WriteLine(e1.GetGuid());
Console.WriteLine(e2.GetGuid());

Console.WriteLine("Singleton Example (duplicate):");
Console.WriteLine(s1.GetGuid());
Console.WriteLine(s2.GetGuid());

Console.WriteLine();
