using Microsoft.Extensions.Logging;
using NServiceBus;
using Server.Data;
using Shared.Requests;
using Shared.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Requesthandler
{
    public class GetCarsRequestHandler : IHandleMessages<GetCarsRequest>
    {
        readonly ICarRepository _carRepository;
        readonly ILogger<GetCarsRequestHandler> _logger;

        public GetCarsRequestHandler(ILogger<GetCarsRequestHandler> logger, ICarRepository carRepository)
        {
            _logger = logger;
            _carRepository = carRepository;
        }

        // TODO: this is not really a good use of messaging, use a call from the API controller to get data from the database
        public async Task Handle(GetCarsRequest message, IMessageHandlerContext context)
        {
            _logger.LogInformation("Received GetCarsRequest.");

            var cars = await _carRepository.GetAllCarsAsync()
                .ConfigureAwait(false);

            var response = new GetCarsResponse()
            {
                DataId = message.DataId,
                Cars = cars.ToList()
            };

            await context.Reply(response)
                .ConfigureAwait(false);
        }
    }
}