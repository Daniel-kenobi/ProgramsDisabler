var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "ProgramsDisabler";
});

var host = builder.Build();
host.Run();
