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
	public class GetCarRequestHandler : IHandleMessages<GetCarRequest>
	{
    readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
    readonly CarApiContext _carApiContext;
    readonly ICarRepository _carRepository;

    public GetCarRequestHandler()
    {
      _dbContextOptionsBuilder = new DbContextOptionsBuilder<CarApiContext>();
      _carApiContext = new CarApiContext(_dbContextOptionsBuilder.Options);
      _carRepository = new CarRepository(_carApiContext);
    }

    static ILog log = LogManager.GetLogger<GetCarRequestHandler>();

    public async Task Handle(GetCarRequest message, IMessageHandlerContext context)
    {
      log.Info("Received GetCarRequest.");

      var car = await _carRepository.GetCarAsync(message.CarId);

      var response = new GetCarResponse()
      {
        DataId = message.DataId,
        Car = car
      };

      await context.Reply(response);
    }
  }
}