using Microsoft.EntityFrameworkCore;
using Shared.Models;
using System.IO;
using System;

namespace Server.DAL
{
	public class CarApiContext : DbContext
	{
		public CarApiContext(DbContextOptions options)
			: base(options)
		{
		}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        var dbFilePath = Path.Combine(Path.Combine(AppContext.BaseDirectory, "App_Data"), "Car.db");
        optionsBuilder.UseSqlite($"Data Source={dbFilePath}");
      }
    }
    public DbSet<Car> Cars { get; set; }

		public DbSet<Company> Companies { get; set; }
	}
}