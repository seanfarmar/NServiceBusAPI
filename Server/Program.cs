﻿using Microsoft.AspNetCore.Hosting;
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
                   var endpointConfiguration = new EndpointConfiguration("NServiceBusCore.Server");

                   endpointConfiguration.ApplyEndpointConfiguration(ctx.Configuration.GetConnectionString("NServiceBusTransport"));

                   return endpointConfiguration;
               })
               .ConfigureServices(services =>
               {
                   services.AddDbContext<CarApiContext>(options => options.UseSqlite($"Data Source={dbFilePath}"));
                   // services.AddTransient<CarApiContext>();
                   services.AddScoped<ICarRepository, CarRepository>();
                   services.AddScoped<ICompanyRepository, CompanyRepository>();
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
