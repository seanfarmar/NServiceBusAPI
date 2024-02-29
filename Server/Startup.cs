using System;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using Microsoft.EntityFrameworkCore;
using Server.Requesthandler;
using Server.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.ComponentModel;

namespace Server
{
	public class Startup
	{
    IContainer ApplicationContainer { get; set; }
    IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<CarApiContext>(options =>
          options.UseSqlite("DataSource=App_Data/Car.db"));

      services.AddTransient<DbContextOptionsBuilder<CarApiContext>>();
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

      // Configure NServiceBus endpoint
      var endpointConfiguration = new EndpointConfiguration("NServiceBusCore.Server");
      endpointConfiguration.EnableCallbacks(makesRequests: false);

      var persistence = endpointConfiguration.UsePersistence<SqlPersistence,StorageType.Sagas>();
      persistence.SqlDialect<SqlDialect.MsSqlServer>();
      var persistenceConnectionString = Configuration.GetConnectionString("NServiceBusPersistence");
      persistence.ConnectionBuilder(
          connectionBuilder: () => new SqlConnection(persistenceConnectionString));

      var transportConnectionString = Configuration.GetConnectionString("NServiceBusTransport");
      endpointConfiguration.UseTransport(new SqlServerTransport(transportConnectionString)
      {
        TransportTransactionMode = TransportTransactionMode.SendsAtomicWithReceive
      });

      var endpointInstance = Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();

      // Register the IEndpointInstance with the service collection
      services.AddSingleton(endpointInstance);
    }
    public void Configure(IApplicationLifetime appLifetime)
    {
      appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
    }
  }
}