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
	public class GetCarResponseHandler : IHandleMessages<GetCarResponse>
	{
    readonly ICarRepository _carRepository;

    public GetCarResponseHandler(ICarRepository carRepository)
    {
      _carRepository = carRepository;
    }

    static ILog log = LogManager.GetLogger<GetCarResponseHandler>();

    public async Task Handle(GetCarResponse message, IMessageHandlerContext context)
    {
      log.Info("Received GetCarResponse.");

      //var car = await _carRepository.GetCarAsync(message.CarId);

      //var response = new GetCarResponse()
      //{
      //  DataId = message.DataId,
      //  Car = car
      //};

      //await context.Reply(response);
    }
  }
}