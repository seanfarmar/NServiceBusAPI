using Shared.Requests;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared.Responses;
using Server.DAL;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;
using System.Collections.Generic;
using System.Linq;

namespace Server.Requesthandler
{
	public class GetCarRequestHandler : IHandleMessages<GetCarRequest>
	{
    readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
    readonly CarUnitOfWork unitOfWork;
    readonly List<Car> cars;
    readonly CarApiContext dbContext;
    public GetCarRequestHandler()
    {
      _dbContextOptionsBuilder = new DbContextOptionsBuilder<CarApiContext>();
      dbContext = new CarApiContext(_dbContextOptionsBuilder.Options);
      unitOfWork = new CarUnitOfWork(dbContext);
      cars = unitOfWork.Cars.GetAll().ToList();
    }

    static ILog log = LogManager.GetLogger<GetCarRequestHandler>();

    public Task Handle(GetCarRequest message, IMessageHandlerContext context)
    {
      log.Info("Received GetCarRequest.");
      Car car;

      car = cars.Where(c => c.Id == message.CarId).SingleOrDefault();

      var response = new GetCarResponse()
      {
        Car = car
      };
      var reply = context.Reply(response);
      return reply;
    }
  }
}