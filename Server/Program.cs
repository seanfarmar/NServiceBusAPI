using System;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace Server
{

	internal class Program
	{
		public static async Task Main(string[] args)
		{
            using var host = CreateHostBuilder(args).Build();
            await host.StartAsync();

            Console.WriteLine("Press any key to shutdown");
            Console.ReadKey();
            await host.StopAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseNServiceBus(c =>
            {
                var endpointConfiguration = new EndpointConfiguration("Sample.Core");
                endpointConfiguration.UseSerialization<SystemJsonSerializer>();
                endpointConfiguration.UseTransport<LearningTransport>();
                return endpointConfiguration;
            }).ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
  }

}
