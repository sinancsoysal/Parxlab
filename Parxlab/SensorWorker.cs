using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Parxlab.Service.Contracts;

namespace Parxlab
{
    public class SensorWorker:IHostedService
    {
        private Timer timer;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public SensorWorker(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.FromSeconds(1), TimeSpan.FromHours(6));
            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            using var scope = serviceScopeFactory.CreateScope();
            var backgroundService = scope.ServiceProvider.GetRequiredService<ISensorManager>(); 
            backgroundService.StartListener("169.254.59.39", 6000);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
           return Task.CompletedTask;
        }
    }
}
