using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Client.DAL
{
	using Client.Models;
  using System.IO;
  using System;

	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        var dbFilePath = Path.Combine(Path.Combine(AppContext.BaseDirectory, "App_Data"), "AspNet.db");
        optionsBuilder.UseSqlite($"Data Source={dbFilePath}");
      }
    }//}
  }
  }