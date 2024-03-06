using Microsoft.Extensions.Logging;
using NServiceBus;
using Server.Data;
using Shared.Requests;
using Shared.Responses;
using System.Threading.Tasks;

namespace Server.Requesthandler
{
    public class GetCompanyRequestHandler : IHandleMessages<GetCompanyRequest>
    {
        readonly ILogger<GetCompanyRequestHandler> _logger;
        readonly ICompanyRepository _companyRepository;

        public GetCompanyRequestHandler(
            ILogger<GetCompanyRequestHandler> logger,
            ICompanyRepository companyRepository)
        {
            _logger = logger;
            _companyRepository = companyRepository;
        }

        // TODO: this is not really a good use of messaging, use a call from the API controller to get data from the database
        public async Task Handle(GetCompanyRequest message, IMessageHandlerContext context)
        {
            _logger.LogInformation("Received GetCompanyRequest");

            var company = await _companyRepository.GetCompanyAsync(message.CompanyId)
                .ConfigureAwait(false);

            var response = new GetCompanyResponse()
            {
                DataId = message.DataId,
                Company = company
            };

            await context.Reply(response)
                .ConfigureAwait(false);
        }
    }
}