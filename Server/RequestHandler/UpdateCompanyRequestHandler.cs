using Microsoft.Extensions.Logging;
using NServiceBus;
using Server.Data;
using Shared.Requests;
using Shared.Responses;
using System.Threading.Tasks;

namespace Server.Requesthandler
{
    public class UpdateCompanyRequestHandler : IHandleMessages<UpdateCompanyRequest>
    {
        readonly ILogger<UpdateCompanyRequestHandler> _logger;
        readonly ICompanyRepository _companyRepository;

        public UpdateCompanyRequestHandler(
            ILogger<UpdateCompanyRequestHandler> logger,
            ICompanyRepository companyRepository)
        {
            _logger = logger;
            _companyRepository = companyRepository;
        }

        public async Task Handle(UpdateCompanyRequest message, IMessageHandlerContext context)
        {
            _logger.LogInformation("Received UpdateCompanyRequest");

            await _companyRepository.UpdateCompanyAsync(message.Company)
                .ConfigureAwait(false);

            var response = new UpdateCompanyResponse()
            {
                DataId = message.DataId,
                Company = message.Company
            };

            await context.Reply(response)
                .ConfigureAwait(false);

        }
    }
}