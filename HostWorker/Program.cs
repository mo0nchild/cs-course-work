using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;

namespace HostWorker
{
    public static class Program : System.Object
    {
        public static void Main(string[] args)
        {
            Microsoft.Extensions.Hosting.IHostBuilder servicehost_builder = Host.CreateDefaultBuilder(args)
                .ConfigureServices((IServiceCollection services) => { services.AddHostedService<ServiceWorker>(); });

            using (var servicehost = servicehost_builder.Build()) servicehost.Run();
        }
    }
}
