using Shared.Requests;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared.Responses;
using Server.Data;
using Microsoft.EntityFrameworkCore;
using Server.DAL;

namespace Server.Requesthandler
{
	public class UpdateCarRequestHandler : IHandleMessages<UpdateCarRequest>
	{
    readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
    readonly CarApiContext _carApiContext;
    readonly ICarRepository _carRepository;

    public UpdateCarRequestHandler()
    {
      _dbContextOptionsBuilder = new DbContextOptionsBuilder<CarApiContext>();
      _carApiContext = new CarApiContext(_dbContextOptionsBuilder.Options);
      _carRepository = new CarRepository(_carApiContext);
    }

    static ILog log = LogManager.GetLogger<UpdateCarRequestHandler>();

    public async Task Handle(UpdateCarRequest message, IMessageHandlerContext context)
    {
      log.Info("Received UpdateCarRequest.");

      await _carRepository.UpdateCarAsync(message.Car);

      var response = new UpdateCarResponse()
      {
        DataId = message.DataId,
        Car = message.Car
      };

      await context.Reply(response);
    }
  }
}