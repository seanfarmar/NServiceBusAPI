using Microsoft.Extensions.Logging;
using NServiceBus;
using Server.Data;
using Shared.Requests;
using Shared.Responses;
using System.Threading.Tasks;

namespace Server.Requesthandler
{
    public class GetCarRequestHandler : IHandleMessages<GetCarRequest>
    {
        readonly ILogger<GetCarRequestHandler> _logger;
        readonly ICarRepository _carRepository;

        public GetCarRequestHandler(
            ILogger<GetCarRequestHandler> logger,
            ICarRepository carRepository)
        {
            _logger = logger;
            _carRepository = carRepository;
        }

        // TODO: this is not really a good use of messaging, use a call from the API controller to get data from the database
        public async Task Handle(GetCarRequest message, IMessageHandlerContext context)
        {
            _logger.LogInformation("Received GetCarRequest.");

            var car = await _carRepository.GetCarAsync(message.CarId)
                .ConfigureAwait(false);

            var response = new GetCarResponse()
            {
                DataId = message.DataId,
                Car = car
            };

            await context.Reply(response)
                .ConfigureAwait(false);
        }
    }
}