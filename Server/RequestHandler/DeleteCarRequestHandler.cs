using Microsoft.Extensions.Logging;
using NServiceBus;
using Server.Data;
using Shared.Requests;
using Shared.Responses;
using System.Threading.Tasks;

namespace Server.Requesthandler
{
    public class DeleteCarRequestHandler : IHandleMessages<DeleteCarRequest>
    {
        readonly ICarRepository _carRepository;
        readonly ILogger<DeleteCarRequestHandler> _logger;

        public DeleteCarRequestHandler(
            ILogger<DeleteCarRequestHandler> logger,
            ICarRepository carRepository)
        {
            _logger = logger;
            _carRepository = carRepository;
        }

        public async Task Handle(DeleteCarRequest message, IMessageHandlerContext context)
        {
            _logger.LogInformation("Received DeleteCarRequest.");

            await _carRepository.RemoveCarAsync(message.CarId)
                .ConfigureAwait(false);

            var response = new DeleteCarResponse()
            {
                DataId = message.DataId,
            };

            await context.Reply(response)
                .ConfigureAwait(false);
        }
    }
}