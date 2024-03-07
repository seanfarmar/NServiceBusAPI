using Shared.Responses;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared.Responses;
using Server.Data;
using Microsoft.EntityFrameworkCore;
using Server.DAL;

namespace Server.ResponseHandlers
{
	public class UpdateCarResponseHandler : IHandleMessages<UpdateCarResponse>
	{
    readonly ICarRepository _carRepository;

    public UpdateCarResponseHandler(ICarRepository carRepository)
    {
      _carRepository = carRepository;
    }

    static ILog log = LogManager.GetLogger<UpdateCarResponseHandler>();

    public async Task Handle(UpdateCarResponse message, IMessageHandlerContext context)
    {
      log.Info("Received UpdateCarResponse.");

      //await _carRepository.UpdateCarAsync(message.Car);

      //var response = new UpdateCarResponse()
      //{
      //  DataId = message.DataId,
      //  Car = message.Car
      //};

      //await context.Reply(response);
    }
  }
}