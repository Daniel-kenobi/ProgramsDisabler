# ProgramsDisabler

A lightweight Windows Service that monitors running processes and automatically terminates a conflicting application when a specific process is detected.

## How It Works

The service runs in the background and periodically checks whether CS2 is running.

When CS2 is detected, it checks if the `Lookupper` process is also running. If both processes are active, `Lookupper` is automatically terminated.

## Use Case

I built this utility to solve a simple problem on my own machine.

CS2 conflicts with another application, and I would often forget to close that application before starting the game. Instead of having to remember to close it manually, I created a Windows Service that handles the process automatically in the background.

## Technologies

* C#
* .NET 10
* .NET Worker Service
* Windows Services
* `BackgroundService`
* `PeriodicTimer`
* `System.Diagnostics.Process`

## Requirements

* Windows
* .NET 10

## Installation

Build the project:

```bash
dotnet build
```

Publish the application:

```bash
dotnet publish -c Release
```

The published application can then be registered as a Windows Service.

## Running

The application can also be executed directly during development:

```bash
dotnet run
```