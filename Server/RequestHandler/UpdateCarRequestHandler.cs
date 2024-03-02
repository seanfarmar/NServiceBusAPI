using Shared.Requests;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared.Responses;
using Server.DAL;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using System;
using System.Linq;

namespace Server.Requesthandler
{
	public class UpdateCarRequestHandler : IHandleMessages<UpdateCarRequest>
	{
    readonly CarUnitOfWork _unitOfWork;
    readonly CarApiContext _dbContext;

    public UpdateCarRequestHandler()
    {
      var dbContextOptionsBuilder = new DbContextOptionsBuilder<CarApiContext>();
          dbContextOptionsBuilder.UseSqlite("DataSource=App_Data/Car.db");
      _dbContext = new CarApiContext(dbContextOptionsBuilder.Options);
      _unitOfWork = new CarUnitOfWork(_dbContext);
    }

    static ILog log = LogManager.GetLogger<UpdateCarRequestHandler>();

    public Task Handle(UpdateCarRequest message, IMessageHandlerContext context)
    {
      log.Info("Received UpdateCarRequest.");
      

      var allCars = _unitOfWork.Cars.GetAll();
      var originalCar = allCars.Where(c => c.Id == message.Car.Id).SingleOrDefault();

      if (originalCar != null)
      {
        if (!Equals(originalCar, message.Car))
        {
          _dbContext.Entry(originalCar).State = EntityState.Detached;
          _dbContext.Attach(message.Car);
          _unitOfWork.Complete();
        }
        // else: No changes, so no need to update
      }
      else
      {
        throw new Exception("Cannot update car, not found in database");
      }


      var response = new UpdateCarResponse
      {
        Car = message.Car
      };
      var reply = context.Reply(response);
      return reply;
    }
  }
}