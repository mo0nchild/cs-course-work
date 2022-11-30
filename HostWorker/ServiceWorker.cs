using ServiceLibrary.DataEncoder;
using ServiceLibrary.DataTransfer;
using ServiceLibrary.ServiceLocatorTool;
using System;
using ServiceLibrary;
using ServiceLibrary.ServiceContracts;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;
using System.Net;

namespace HostWorker
{
    public class ServiceWorker : BackgroundService
    {
        private ILogger<HostWorker.ServiceWorker> ServiceLogger { get; set; } = default;

        public ServiceWorker(ILogger<HostWorker.ServiceWorker> service_logger)
        {
            this.ServiceLogger = service_logger;

            ServiceLocator.RegistrationService<INetworkTransfer<SmptMessageEnvelope>>(new SmtpTransferFactory());
            ServiceLocator.RegistrationService<IServiceDataEncoder<ProjectInfo>>(new ServiceDataEncoderFactory());
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var servicehost_instance = new System.ServiceModel.ServiceHost(typeof(ServiceLibrary.GraphService)))
            {
                servicehost_instance.Open();
                this.ServiceLogger.LogInformation($"Сервис был запущен [{DateTime.UtcNow}]");

                foreach (var address in servicehost_instance.BaseAddresses) this.ServiceLogger.LogInformation(address.ToString());
                while (stoppingToken.IsCancellationRequested != true) { await Task.Delay(200, stoppingToken); }
            }
            await Task.CompletedTask;
        }
    }
}