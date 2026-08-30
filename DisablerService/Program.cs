using DisablerService;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<Worker>().AddWindowsService(options =>
{
    options.ServiceName = "ProgramsDisabler";
});

var host = builder.Build();
host.Run();
