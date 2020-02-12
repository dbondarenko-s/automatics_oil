using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AS.Oil.BLL.Provider;
using AS.Oil.BLL.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AS.Oil.Presentation.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.RegisterDbContext(hostContext.Configuration.GetConnectionString("DefaultConnection"), "AS.Oil.Migration");
                    services.RegisterCollection();
                    services.AddHostedService<QueueWorker>();
                });
    }
}
