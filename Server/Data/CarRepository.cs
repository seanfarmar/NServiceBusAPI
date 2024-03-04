using Shared.Models;
using Server.DAL;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;

namespace Server.Data
{
  public class CarRepository : ICarRepository
  {
    private readonly CarApiContext _context;
    public CarRepository(CarApiContext context)
    {
      _context = context;

    }

    public async Task<int> AddCarAsync(Car car)
    {
      _context.Cars.Add(car);
      return await _context.SaveChangesAsync();
    }

    public async Task<int> RemoveCarAsync(Guid Id)
    {
      var cars = await GetAllCarsAsync();
      var foundCar = cars.Where(c => c.Id == Id).FirstOrDefault() ?? throw new Exception($"Car with id '{Id}' not found in database");
      _context.Cars.Remove(foundCar);
      return await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Car>> GetAllCarsAsync()
    {
      return await _context.Cars.ToListAsync();
    }

    public async Task<Car> GetCarAsync(Guid Id)
    {
      var cars = await GetAllCarsAsync();
      return cars.Where(c => c.Id == Id).SingleOrDefault();
    }

    public async Task<int> SaveContextChanges()
    {
      return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateCarAsync(Car car)
    {
      // Retrieve the original entity from the database
      var original = await GetCarAsync(car.Id);

      // Check if the original entity exists
      if (original == null)
      {
        throw new Exception($"Car with id '{car.Id}' not found in database");
      }

      // Update the properties of the original entity with the new values
      _context.Entry(original).CurrentValues.SetValues(car);

      try
      {
        // Save the changes to the database
        return await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        // Handle concurrency conflicts
        // You may want to retry the operation or implement custom logic here
        throw;
      }
    }
  }
}
