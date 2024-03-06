using Microsoft.Extensions.Logging;
using NServiceBus;
using Server.Data;
using Shared.Requests;
using Shared.Responses;
using System.Threading.Tasks;

namespace Server.Requesthandler
{
    public class DeleteCompanyRequestHandler : IHandleMessages<DeleteCompanyRequest>
    {
        readonly ILogger<DeleteCompanyRequestHandler> _logger;
        readonly ICompanyRepository _companyRepository;

        public DeleteCompanyRequestHandler(
            ILogger<DeleteCompanyRequestHandler> logger,
            ICompanyRepository companyRepository)
        {
            _logger = logger;
            _companyRepository = companyRepository;
        }

        public async Task Handle(DeleteCompanyRequest message, IMessageHandlerContext context)
        {
            _logger.LogInformation("Received DeleteCompanyRequest.");

            await _companyRepository.RemoveCompanyAsync(message.CompanyId)
                .ConfigureAwait(false);

            var response = new DeleteCompanyResponse()
            {
                DataId = message.DataId,
            };

            await context.Reply(response)
                .ConfigureAwait(false);
        }
    }
}