﻿using Microsoft.EntityFrameworkCore;
using Server.DAL;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Data
{
    public class CarRepository : ICarRepository
    {
        private readonly CarApiContext _context;
        private readonly SemaphoreSlim _asyncLock = new SemaphoreSlim(1, 1);

        public CarRepository(CarApiContext context)
        {
            _context = context;
        }

        public async Task<int> AddCarAsync(Car car)
        {
            await _asyncLock.WaitAsync();
            try
            {
                _context.Cars.Add(car);
                return await _context.SaveChangesAsync();
            }
            finally
            {
                _asyncLock.Release();
            }
        }

        public async Task<int> RemoveCarAsync(Guid Id)
        {
            await _asyncLock.WaitAsync();
            try
            {
                var foundCar = await _context.Cars.Where(c => c.Id == Id).SingleOrDefaultAsync() ?? throw new Exception($"Car with id '{Id}' not found in database");
                _context.Cars.Remove(foundCar);
                return await _context.SaveChangesAsync();
            }
            finally
            {
                _asyncLock.Release();
            }
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetCarAsync(Guid Id)
        {
            return await _context.Cars.Where(c => c.Id == Id).SingleOrDefaultAsync();
        }

        public async Task<int> SaveContextChanges()
        {
            await _asyncLock.WaitAsync();
            try
            {
                return await _context.SaveChangesAsync();
            }
            finally
            {
                _asyncLock.Release();
            }
        }

        public async Task<int> UpdateCarAsync(Car car)
        {
            await _asyncLock.WaitAsync();
            try
            {
                var original = await GetCarAsync(car.Id) ?? throw new Exception($"Car with id '{car.Id}' not found in database");
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
            finally
            {
                _asyncLock.Release();
            }
        }
    }
}