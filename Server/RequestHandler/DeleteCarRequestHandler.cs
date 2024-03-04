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
	public class DeleteCarRequestHandler : IHandleMessages<DeleteCarRequest>
	{
    readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
    readonly CarApiContext _carApiContext;
    readonly ICarRepository _carRepository;

    public DeleteCarRequestHandler()
    {
      _dbContextOptionsBuilder = new DbContextOptionsBuilder<CarApiContext>();
      _carApiContext = new CarApiContext(_dbContextOptionsBuilder.Options);
      _carRepository = new CarRepository(_carApiContext);
    }

    static ILog log = LogManager.GetLogger<DeleteCarRequestHandler>();

    public async Task Handle(DeleteCarRequest message, IMessageHandlerContext context)
    {
      log.Info("Received DeleteCarRequest.");
      
      await _carRepository.RemoveCarAsync(message.CarId);

      var response = new DeleteCarResponse()
      {
        DataId = message.DataId,
      };

      await context.Reply(response);
    }
  }
}