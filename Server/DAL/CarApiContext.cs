using Microsoft.EntityFrameworkCore;
using Shared.Models;

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
        // Configure the context if it hasn't been configured explicitly
        optionsBuilder.UseSqlite("DataSource=App_Data/Car.db");
      }
    }
    public DbSet<Car> Cars { get; set; }

		public DbSet<Company> Companies { get; set; }
	}
}