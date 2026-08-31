using DisablerService.Infraestructure;

var builder = Host.CreateApplicationBuilder(args);

builder.BuildService().BuildOptions();

var host = builder.Build();
host.Run();