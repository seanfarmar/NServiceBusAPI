using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NServiceBus;
using Client.DAL;
using Client.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Client.Models;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using NServiceBus.Transport.SqlServer;

namespace Client
{
	public class Startup
	{
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }
		public IConfiguration Configuration { get; }
    IEndpointInstance EndpointInstance { get; set; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
      var endpointConfiguration = new EndpointConfiguration("NServiceBusCore.Client");
      endpointConfiguration.SendFailedMessagesTo("error");
      endpointConfiguration.EnableInstallers();

      var connectionString = Configuration.GetConnectionString("NServiceBusTransport");
      var transport = new SqlServerTransport(connectionString)
      {
        DefaultSchema = "dbo",
        Subscriptions =
            {
                SubscriptionTableName = new SubscriptionTableName(
                        table: "Subscriptions",
                        schema: "dbo")
            },
        TransportTransactionMode = TransportTransactionMode.SendsAtomicWithReceive
      };

      transport.SchemaAndCatalog.UseSchemaForQueue("error", "dbo");
      transport.SchemaAndCatalog.UseSchemaForQueue("audit", "dbo");
      transport.SchemaAndCatalog.UseSchemaForQueue("NServiceBusCore.Client", "dbo");

      endpointConfiguration.UseSerialization<SystemJsonSerializer>();
      var routing = endpointConfiguration.UseTransport(transport);

      endpointConfiguration.MakeInstanceUniquelyAddressable("1");
      //routing.RouteToEndpoint(typeof(Request), "NServiceBusCore.Server");
      endpointConfiguration.EnableCallbacks();
      EndpointInstance = Endpoint.Start(endpointConfiguration).ConfigureAwait(false).GetAwaiter().GetResult();

      services.AddSingleton(EndpointInstance);

			services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			//// Add application services.
			services.AddTransient<IEmailSender, EmailSender>();

			services.AddMvc();

			var task = ConfigureServicesAsync(services);

			task.Wait();

		}

		async Task ConfigureServicesAsync(IServiceCollection services)
		{
			string aspNetDb = null;
			var aspNetDbLocation = new AspNetDbLocation();
			try
			{
				var getAspNetDb = await aspNetDbLocation.GetAspNetDbAsync(EndpointInstance);
				aspNetDb = getAspNetDb.AspNetDb;
			}
			catch (Exception e)
			{
				//Do nothing
			}
			if (aspNetDb != null)
			{
				services.AddDbContext<ApplicationDbContext>(options =>
					options.UseSqlite("Data Source=" + aspNetDb));
			}
			else
			{
				services.AddDbContext<ApplicationDbContext>(options =>
					options.UseSqlite("Data Source=" + Directory.GetCurrentDirectory() + "\\App_Data\\AspNet.db"));
			}
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }
      app.UseStaticFiles();

			app.UseAuthentication();

      app.UseRouting(); // Use routing middleware

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
      });
    }
	}
}