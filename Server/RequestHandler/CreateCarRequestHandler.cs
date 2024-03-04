using Shared.Requests;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared.Responses;
using Server.Data;
using Microsoft.EntityFrameworkCore;
using Server.DAL;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Server.Requesthandler
{
  public class CreateCarRequestHandler : IHandleMessages<CreateCarRequest>
  {
    readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
    readonly CarApiContext _carApiContext;
    readonly ICarRepository _carRepository;
    public CreateCarRequestHandler()
    {
      _dbContextOptionsBuilder = new DbContextOptionsBuilder<CarApiContext>();
      _carApiContext = new CarApiContext(_dbContextOptionsBuilder.Options);
      _carRepository = new CarRepository(_carApiContext);
    }

    static ILog log = LogManager.GetLogger<CreateCarRequestHandler>();

		public async Task Handle(CreateCarRequest message, IMessageHandlerContext context)
		{
			log.Info("Received CreateCarRequest.");

			var response = new CreateCarResponse()
			{
        DataId = message.DataId,
        Car = message.Car
			};

			await _carRepository.AddCarAsync(message.Car);

		  await context.Reply(response);
		}
	}
}