using System.Linq;
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
	public class GetCarsResponseHandler : IHandleMessages<GetCarsResponse>
	{
    readonly ICarRepository _carRepository;

    public GetCarsResponseHandler(ICarRepository carRepository)
    {
            _carRepository = carRepository;
    }

    static ILog log = LogManager.GetLogger<GetCarsResponseHandler>();

		public async Task Handle(GetCarsResponse message, IMessageHandlerContext context)
		{
			log.Info("Received GetCarsResponse.");

      //var cars = await _carRepository.GetAllCarsAsync();

      //var response = new GetCarsResponse()
      //{
      //  DataId = message.DataId,
      //  Cars = cars.ToList()
      //};

      //await context.Reply(response);
    }
	}
}