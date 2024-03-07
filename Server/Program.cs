using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NServiceBus;
using Server.DAL;
using Server.Data;
using Shared.Particular;
using System;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            CultureInfo.CurrentUICulture = new CultureInfo("en-US");
            var builder = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json");

            var app = CreateHostBuilder(args).Build();

            using (var scope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<CarApiContext>().EnsureSeedData();
            }

            await app.RunAsync();
        }

        static IHostBuilder CreateHostBuilder(string[] args)
        {
            var dbFilePath = Path.Combine(Path.Combine(AppContext.BaseDirectory, "App_Data"), "Car.db");

            return Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                })
               .UseNServiceBus(ctx =>
               {
                   string endpointName = "NServiceBusCore.Server";

                   var endpointConfiguration = new EndpointConfiguration(endpointName);

                   endpointConfiguration.ApplyEndpointConfiguration(
                       ctx.Configuration.GetConnectionString("NServiceBusTransport"),
                       endpointName,
                       EndpointMappings.MessageEndpointMappings());

                   return endpointConfiguration;
               })
               .ConfigureServices(services =>
               {
                   // TODO: this should be in the same database server as the transport if using SQL
                   services.AddDbContext<CarApiContext>(options => options.UseSqlite($"Data Source={dbFilePath}"));
                   services.AddTransient<ICarRepository, CarRepository>();
                   services.AddTransient<ICompanyRepository, CompanyRepository>();
               });
        }
    }
}


//  static IWebHost BuildWebHost(string[] args) =>
//    WebHost.CreateDefaultBuilder(args)
//      .UseStartup<Startup>()
//      .Build();
//}

//}
