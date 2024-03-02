using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Server.DAL
{

	public class CarDataAccess
	{
		readonly CarApiContext _dbContext;

		public CarDataAccess(CarApiContext dbContext)
        {
			_dbContext = dbContext;
        }

	    public ICollection<Car> GetCars()
	    {
		    using (var context = _dbContext)
		    {
			    return context.Cars.ToList();
		    }
	    }

	    public Car GetCar(Guid id)
	    {
		    using (var context = _dbContext)
		    {
			    return context.Cars.SingleOrDefault(o => o.Id == id);
		    }
	    }

	    public void AddCar(Car car)
	    {
		    using (var context = _dbContext)
		    {
			    context.Cars.Add(car);
			    context.SaveChanges();
		    }
	    }

	    public void DeleteCar(Guid id)
	    {
		    using (var context = _dbContext)
		    {
			    var Car = GetCar(id);
			    context.Cars.Remove(Car);
			    context.SaveChanges();
		    }
	    }

		public void UpdateCar(Car car)
		{
			using (var context = _dbContext)
			{
				context.Cars.Update(car);
				context.SaveChanges();
			}
		}

		public ICollection<Company> GetCompanies()
        {
            using (var context = _dbContext)
            {
                return context.Companies.ToList();
            }
        }

        public Company GetCompany(Guid id)
        {
            using (var context = _dbContext)
            {
                return context.Companies.SingleOrDefault(o => o.Id == id);
            }
        }

        public void AddCompany(Company company)
        {
            using (var context = _dbContext)
            {
                context.Companies.Add(company);
                context.SaveChanges();
            }
        }

        public void DeleteCompany(Guid id)
        {
            using (var context = _dbContext)
            {
                var company = GetCompany(id);
                context.Companies.Remove(company);
                context.SaveChanges();
            }
        }

		public void UpdateCompany(Company company)
		{
			using (var context = _dbContext)
			{
				context.Companies.Update(company);
				context.SaveChanges();
			}
		}

  }
}