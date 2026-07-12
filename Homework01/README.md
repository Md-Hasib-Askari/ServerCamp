# Homework01

A small from-scratch implementation of a dependency injection (DI) container, written in C# (.NET 10).

## Overview

This project builds a minimal IoC container that mimics the shape of `Microsoft.Extensions.DependencyInjection`:

- Register service/implementation pairs with a lifetime (`Transient`, `Singleton`, `Scoped`).
- Build a `ServiceProvider` from the registrations.
- Resolve services on demand, with correct lifetime behavior.

## Project structure

```
Homework01/
├── Core/                       # The DI container implementation
│   ├── Enums/ServiceLifetime.cs
│   ├── Interfaces/             # IServiceCollection, IServiceProvider
│   ├── Models/ServiceDescriptor.cs
│   ├── ServiceCollection.cs    # Registration API
│   └── ServiceProvider.cs      # Resolution + scopes
├── Interfaces/                 # Application service contracts
│   ├── IEmailService.cs
│   ├── IMyService.cs
│   └── IScopedService.cs
├── Services/                   # Application service implementations
│   ├── EmailService.cs
│   ├── MyService.cs
│   └── ScopedService.cs
├── Program.cs                  # Demo / entry point
└── Homework01.csproj
```

## Lifetimes

| Lifetime   | Behavior                                                            |
|------------|---------------------------------------------------------------------|
| `Transient`| A new instance is created on every resolution.                      |
| `Singleton`| A single shared instance is created once and reused.               |
| `Scoped`   | One instance per scope; resolved via `CreateScope()`.              |

## Running

```bash
dotnet run --project Homework01
```

`Program.cs` demonstrates each lifetime by resolving services and printing their GUIDs, showing that:

- Transient resolutions produce different GUIDs.
- Singleton resolutions (including the duplicate `IMyService` registration) reuse the same instance.
- Scoped resolutions share an instance within a scope but differ across scopes.
