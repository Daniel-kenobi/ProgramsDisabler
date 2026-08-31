using DisablerService.Core.Options;

namespace DisablerService.Infraestructure;

public static class ServicesConfiguration
{
    public static HostApplicationBuilder BuildService(this HostApplicationBuilder builder)
    {
        builder.Services.AddHostedService<Worker>().AddWindowsService(options =>
        {
            options.ServiceName = "ProgramsDisabler";
        });

        return builder;
    }

    public static HostApplicationBuilder BuildOptions(this HostApplicationBuilder builder)
    {
        builder.Services.Configure<ProcessOptions>(
            builder.Configuration.GetSection("ProcessOptions")
        );

        return builder;
    }
}
