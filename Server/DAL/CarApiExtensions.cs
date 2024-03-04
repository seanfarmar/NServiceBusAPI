﻿using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Models;

namespace Server.DAL
{


	public static class CarApiExtensions
	{
		public static void EnsureSeedData(this CarApiContext context)
		{
			if (!context.Cars.Any() || !context.Companies.Any())
			{
				var companyId = Guid.NewGuid();
				context.Companies.Add(new Company(companyId)
				{
					Name = "Charlies Gravel Transports Ltd.",
					Address = "Concrete Road 8, 111 11 Newcastle"
				});
				context.Cars.Add(new Car(companyId)
				{
					VIN = "YS2R4X20005399401",
					RegNr = "ABC123"
				});
				context.Cars.Add(new Car(companyId)
				{
					VIN = "VLUR4X20009093588",
					RegNr = "DEF456"
				});
				context.Cars.Add(new Car(companyId)
				{
					VIN = "VLUR4X20009048066",
					RegNr = "GHI789"
				});

				companyId = Guid.NewGuid();
				context.Companies.Add(new Company(companyId) { Name = "Jonnies Bulk Ltd.", Address = "Balk Road 12, 222 22 London" });
				context.Cars.Add(new Car(companyId)
				{
					VIN = "YS2R4X20005388011",
					RegNr = "JKL012"
				});
				context.Cars.Add(new Car(companyId)
				{
					VIN = "YS2R4X20005387949",
					RegNr = "MNO345"
				});

				companyId = Guid.NewGuid();
				context.Companies.Add(new Company(companyId) { Name = "Harolds Road Transports Ltd.", Address = "Budget Avenue 1, 333 33 Birmingham" });
				context.Cars.Add(new Car(companyId)
				{
					VIN = "YS2R4X20005387765",
					RegNr = "PQR678"
				});
				context.Cars.Add(new Car(companyId)
				{
					VIN = "YS2R4X20005387055",
					RegNr = "STU901"
				});
			}
			else
			{
				foreach (var car in context.Cars)
				{
					car.Disabled = false;
				}
			}
			context.SaveChanges();
		}

    static public void InitSqLiteDb()
    {
      DbContextOptionsBuilder<CarApiContext> _optionsBuilder = new DbContextOptionsBuilder<CarApiContext>();
      var dbFilePath = Path.Combine(Path.Combine(AppContext.BaseDirectory, "App_Data"), "Car.db");
      _optionsBuilder.UseSqlite($"Data Source={dbFilePath}");
      
			using (var context = new CarApiContext(_optionsBuilder.Options))
      {
        context.Database.EnsureCreated();
        context.EnsureSeedData();
      }
    }
  }
}
