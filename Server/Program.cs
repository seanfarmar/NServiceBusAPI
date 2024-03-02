using Microsoft.AspNetCore.Hosting;
using System.Globalization;
using Microsoft.AspNetCore;
using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Server
{

	internal class Program
	{
		public static void Main(string[] args)
		{
			CultureInfo.CurrentUICulture = new CultureInfo("en-US");
      var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json");

      builder.Build();

      BuildWebHost(args).Run();
    }

    static IWebHost BuildWebHost(string[] args) =>
      WebHost.CreateDefaultBuilder(args)
        .UseKestrel()
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration()
        .UseStartup<Startup>()
        .ConfigureLogging((hostingContext, logging) =>
         {
            logging.ClearProviders();
         })
        .Build();
  }

}
