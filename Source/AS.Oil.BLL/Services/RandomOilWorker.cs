using AS.Oil.BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AS.Oil.BLL.Services
{
    public class RandomOilWorker : BackgroundService
    {
        private readonly ILogger<RandomOilWorker> _logger;
        private readonly IStorageService _storageService;
        private readonly IServiceScope _serviceScope;

        public RandomOilWorker(IServiceProvider services, ILogger<RandomOilWorker> logger)
        {
            _logger = logger;

            _serviceScope = services.CreateScope();

            _storageService = _serviceScope.ServiceProvider.GetRequiredService<IStorageService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("RandomOilWorker running at: {time}", DateTimeOffset.Now);

                foreach(var item in await _storageService.GetKeyValuePairIdAndVolumeAsync())
                {
                    QueueService.Add(new Models.QueueItem { Type = Enums.OperationType.SetVolume, Storage = new Models.DTO.StorageDto { Id = item.Key, Volume = item.Value } });
                }

                await Task.Delay(2500, stoppingToken);
            }
        }
    }
}
