using Microsoft.Extensions.Logging;
using NServiceBus;
using Server.Data;
using Shared.Requests;
using Shared.Responses;
using System.Threading.Tasks;

namespace Server.Requesthandler
{
    public class CreateCarRequestHandler : IHandleMessages<CreateCarRequest>
    {
        // readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
        readonly ICarRepository _carRepository;
        readonly ILogger<CreateCarRequestHandler> _logger;

        public CreateCarRequestHandler(
            ILogger<CreateCarRequestHandler> logger,
            ICarRepository carRepository)
        {
            _logger = logger;
            _carRepository = carRepository;
        }

        public async Task Handle(CreateCarRequest message, IMessageHandlerContext context)
        {
            _logger.LogInformation("Received CreateCarRequest.");

            await _carRepository.AddCarAsync(message.Car)
                .ConfigureAwait(false);

            var response = new CreateCarResponse()
            {
                DataId = message.DataId,
                Car = message.Car
            };

            await context.Reply(response)
                .ConfigureAwait(false);
        }
    }
}