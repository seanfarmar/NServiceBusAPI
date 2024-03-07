using Shared.Requests;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared.Responses;
using Server.Data;
using Microsoft.EntityFrameworkCore;
using Server.DAL;

namespace Server.ResponseHandlers
{
  public class CreateCarResponseHandler : IHandleMessages<CreateCarResponse>
  {
    readonly ICarRepository _carRepository;

    public CreateCarResponseHandler(ICarRepository carRepository)
    {
      _carRepository = carRepository;
    }

    static ILog log = LogManager.GetLogger<CreateCarResponseHandler>();

		public async Task Handle(CreateCarResponse message, IMessageHandlerContext context)
		{
			log.Info("Received CreateCarResponse.");

   //   await _carRepository.AddCarAsync(message.Car);

   //   var response = new CreateCarResponse()
			//{
   //     DataId = message.DataId,
   //     Car = message.Car
			//};

		 // await context.Reply(response);
		}
	}
}