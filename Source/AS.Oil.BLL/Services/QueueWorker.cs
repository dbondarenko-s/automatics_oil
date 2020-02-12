using AS.Oil.BLL.Enums;
using AS.Oil.BLL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AS.Oil.BLL.Services
{
    public class QueueWorker : BackgroundService
    {
        private readonly ILogger<QueueWorker> _logger;
        private readonly IStorageService _storageService;
        private readonly IServiceScope _serviceScope;

        public QueueWorker(IServiceProvider services, ILogger<QueueWorker> logger)
        {
            _logger = logger;

            _serviceScope = services.CreateScope();

            _storageService = _serviceScope.ServiceProvider.GetRequiredService<IStorageService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("QueueWorker running at: {time}", DateTimeOffset.Now);

                while(QueueService.HasItem)
                {
                    var item = QueueService.Get();

                    if (item != null)
                    {
                        switch (item.Type)
                        {
                            case OperationType.Insert:
                                _logger.LogInformation($"Инициализация операции добавления резервуара ...");
                                await _storageService.AddAsync(item.Storage);
                                break;

                            case OperationType.Update:
                                _logger.LogInformation($"Инициализация операции редактирования данных резервуара ...");
                                await _storageService.EditAsync(item.Storage);
                                break;

                            case OperationType.Delete:
                                _logger.LogInformation($"Инициализация операции удаления резервуара ...");
                                await _storageService.DeleteAsync(item.Storage.Id);
                                break;

                            case OperationType.SetVolume:
                                _logger.LogInformation($"Инициализация операции редактирования данных резервуара ...");
                                await _storageService.SetVolumeAsync(item.Storage.Id, item.Storage.Volume);
                                break;
                        }
                    }
                    else
                    {
                        _logger.LogInformation($"Не удалось получить данные из очереди");
                    }
                }

                await Task.Delay(100, stoppingToken);
            }
        }
    }
}
