using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NServiceBus;
using NServiceBus.Transport.SqlServer;
using Server.DAL;
using Server.Requesthandler;
using Shared;
using System;
using System.ComponentModel;
using System.IO;

namespace Server
{
  public class Startup
	{
    IContainer ApplicationContainer { get; set; }
    IConfiguration Configuration { get; }

    IEndpointInstance EndpointInstance { get; set; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
      var dbFilePath = Path.Combine(Path.Combine(AppContext.BaseDirectory, "App_Data"), "Car.db");

      var dbContextOptionsBuilder = new DbContextOptionsBuilder<CarApiContext>();
      dbContextOptionsBuilder.UseSqlite($"Data Source={dbFilePath}");

      services.AddDbContext<CarApiContext>(options =>
          options.UseSqlite($"Data Source={dbFilePath}"));

      services.AddTransient<CreateCarRequestHandler>();
      services.AddTransient<CreateCompanyRequestHandler>();
      services.AddTransient<DeleteCarRequestHandler>();
      services.AddTransient<DeleteCompanyRequestHandler>();
      services.AddTransient<GetCarRequestHandler>();
      services.AddTransient<GetCarsRequestHandler>();
      services.AddTransient<GetCompanyRequestHandler>();
      services.AddTransient<GetCompaniesRequestHandler>();
      services.AddTransient<UpdateCarRequestHandler>();
      services.AddTransient<UpdateCompanyRequestHandler>();

      CarApiExtensions.InitSqLiteDb();
      
      // Configure NServiceBus endpoint
      var endpointConfiguration = new EndpointConfiguration("NServiceBusCore.Server");
      endpointConfiguration.SendFailedMessagesTo("error");
      endpointConfiguration.AuditProcessedMessagesTo("audit");
      endpointConfiguration.EnableInstallers();

      var connectionString = Configuration.GetConnectionString("NServiceBusTransport");

      var transport = new SqlServerTransport(connectionString)
      {
        DefaultSchema = "dbo",
        TransportTransactionMode = TransportTransactionMode.SendsAtomicWithReceive,
        Subscriptions =
            {
                CacheInvalidationPeriod = TimeSpan.FromMinutes(1),
                SubscriptionTableName = new SubscriptionTableName(table: "Subscriptions", schema: "dbo")
            }
      };

      transport.SchemaAndCatalog.UseSchemaForQueue("error", "dbo");
      transport.SchemaAndCatalog.UseSchemaForQueue("audit", "dbo");
      transport.SchemaAndCatalog.UseSchemaForQueue("NServiceBusCore.Client", "client");

      endpointConfiguration.UseSerialization<SystemJsonSerializer>();
      var routing = endpointConfiguration.UseTransport(transport);
      SqlHelper.CreateSchema(connectionString, "server").GetAwaiter().GetResult();
      endpointConfiguration.MakeInstanceUniquelyAddressable("1");
      //routing.RouteToEndpoint(typeof(Response), "NServiceBusCore.Client");



      //var allText = File.ReadAllText("Startup.sql");
      //await SqlHelper.ExecuteSql(connectionString, allText);
      EndpointInstance = Endpoint.Start(endpointConfiguration).ConfigureAwait(false).GetAwaiter().GetResult();
      services.AddSingleton(EndpointInstance);
      //Console.WriteLine("Server, Press any key to exit");
      //Console.ReadKey();
      //await EndpointInstance.Stop();
      //var serviceProvider = services.BuildServiceProvider();
      //var someService = serviceProvider.GetService<DbContextOptionsBuilder<CarApiContext>>();
    }

    public void Configure(IApplicationLifetime appLifetime)
    {
      appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
    }
  }
}