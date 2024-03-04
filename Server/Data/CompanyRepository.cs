using Shared.Models;
using Server.DAL;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Runtime.ConstrainedExecution;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Server.Data
{
  public class CompanyRepository : ICompanyRepository
  {
    private readonly CarApiContext _context;
    public CompanyRepository(CarApiContext context)
    {
      _context = context;

    }

    public async Task<int> AddCompanyAsync(Company company)
    {
      _context.Companies.Add(company);
      return await _context.SaveChangesAsync();
    }

    public async Task<int> RemoveCompanyAsync(Guid Id)
    {
      var companies = await GetAllCompaniesAsync();
      var foundCompany = companies.Where(c => c.Id == Id).FirstOrDefault() ?? throw new Exception($"Company with id '{Id}' not found in database");
      _context.Companies.Remove(foundCompany);
      return await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Company>> GetAllCompaniesAsync()
    {
      return await _context.Companies.ToListAsync();
    }

    public async Task<Company> GetCompanyAsync(Guid Id)
    {
      var companies = await GetAllCompaniesAsync();
      return companies.Where(c => c.Id == Id).SingleOrDefault();
    }

    public async Task<int> SaveContextChanges()
    {
      return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateCompanyAsync(Company company)
    {
      // Retrieve the original entity from the database
      var original = await GetCompanyAsync(company.Id);

      // Check if the original entity exists
      if (original == null)
      {
        throw new Exception($"Company with id '{company.Id}' not found in database");
      }

      // Update the properties of the original entity with the new values
      _context.Entry(original).CurrentValues.SetValues(company);

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
