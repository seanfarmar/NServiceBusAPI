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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Server.Requesthandler
{
	public class UpdateCarRequestHandler : IHandleMessages<UpdateCarRequest>
	{
		readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
		public UpdateCarRequestHandler()
		{
			_dbContextOptionsBuilder = new DbContextOptionsBuilder<CarApiContext>();
      _dbContextOptionsBuilder.EnableSensitiveDataLogging();

    }

		static ILog log = LogManager.GetLogger<UpdateCarRequestHandler>();

    public Task Handle(UpdateCarRequest message, IMessageHandlerContext context)
    {
      log.Info("Received UpdateCarRequest.");
      using var unitOfWork = new CarUnitOfWork(new CarApiContext(_dbContextOptionsBuilder.Options));

      var existingCar = unitOfWork.Cars.Get(message.Car.Id);

      if (existingCar != null)
      {
        if (!Equals(existingCar, message.Car))
        {
          existingCar = message.Car;
          unitOfWork.Cars.Update(existingCar);
          unitOfWork.Complete();
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