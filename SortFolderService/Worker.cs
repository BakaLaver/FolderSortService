using SortSettings.Entities;
using SortSettings.Entities.Abstractions;
using SortSettings.Settings;

namespace SortFolderService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private ISorter _sort;
        private Options _options;
        private Manager _manager;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            SetSettings();
        }
        private void SetSettings()
        {
            _options = new Options();
            _sort = new Sorter();
            var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _options.Path = builder.Build().GetSection("Path").Value;
            _options.Folders = builder.Build().GetSection("Folders").Get<List<Folders>>();


        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                _manager = new Manager(_sort, _options);
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}