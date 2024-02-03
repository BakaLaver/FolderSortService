using Microsoft.Extensions.Logging.EventLog;
using SortFolderService;

IHostBuilder builder = Host.CreateDefaultBuilder(args);
builder.ConfigureLogging((context, options) =>
{
    if (OperatingSystem.IsWindows())
    {
        options.AddFilter<EventLogLoggerProvider>(Level => Level >= context.Configuration.GetSection("EventLogLoggerProviderLogLevel").Get<LogLevel>());
    }
    options.AddConfiguration(context.Configuration.GetSection("Logging"));
});
builder.ConfigureServices((context, services) =>

{
    services.AddHostedService<Worker>();
    if (OperatingSystem.IsWindows())
    {
        services.Configure<EventLogSettings>(config =>
        {
            if (OperatingSystem.IsWindows())
            {
                config.LogName = context.Configuration.GetSection("Logging:EventLog:LogName").Value ?? "Application";
                config.SourceName = context.Configuration.GetSection("Logging:EventLog:SourceName").Value ?? "Sorter Manager Service";
            }
        });
    }
})

    .UseWindowsService(options =>
    {
        options.ServiceName = "Sorter Manager Service";

    });//https://learn.microsoft.com/en-us/dotnet/core/extensions/windows-service?source=recommendations
//dotnet-FolderSortService-8c28bfac-dad5-4ce8-b243-879e285a47b6
IHost host = builder.Build();
await host.RunAsync();
