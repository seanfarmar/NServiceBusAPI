using Microsoft.Extensions.Logging;
using NServiceBus;
using Server.Data;
using Shared.Requests;
using Shared.Responses;
using System.Threading.Tasks;

namespace Server.Requesthandler
{
    public class UpdateCarRequestHandler : IHandleMessages<UpdateCarRequest>
    {
        readonly ILogger<UpdateCarRequestHandler> _logger;
        readonly ICarRepository _carRepository;

        public UpdateCarRequestHandler(
            ILogger<UpdateCarRequestHandler> logger,
            ICarRepository carRepository)
        {
            _logger = logger;
            _carRepository = carRepository;
        }

        public async Task Handle(UpdateCarRequest message, IMessageHandlerContext context)
        {
            _logger.LogInformation("Received UpdateCarRequest.");

            await _carRepository.UpdateCarAsync(message.Car)
                .ConfigureAwait(false);

            var response = new UpdateCarResponse()
            {
                DataId = message.DataId,
                Car = message.Car
            };

            await context.Reply(response)
                .ConfigureAwait(false);
        }
    }
}