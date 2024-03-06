using Microsoft.Extensions.Logging;
using NServiceBus;
using Server.Data;
using Shared.Requests;
using Shared.Responses;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Requesthandler
{
    public class GetCompaniesRequestHandler : IHandleMessages<GetCompaniesRequest>
    {
        readonly ILogger<GetCompaniesRequestHandler> _logger;
        readonly ICompanyRepository _companyRepository;

        public GetCompaniesRequestHandler(
            ILogger<GetCompaniesRequestHandler> logger,
            ICompanyRepository companyRepository)
        {
            _logger = logger;
            _companyRepository = companyRepository;
        }

        // TODO: this is not really a good use of messaging, use a call from the API controller to get data from the database
        public async Task Handle(GetCompaniesRequest message, IMessageHandlerContext context)
        {
            _logger.LogInformation("Received GetCompaniesRequest");

            var companies = await _companyRepository.GetAllCompaniesAsync()
                .ConfigureAwait(false);

            var response = new GetCompaniesResponse()
            {
                DataId = message.DataId,
                Companies = companies.ToList()
            };

            await context.Reply(response)
                .ConfigureAwait(false);
        }
    }
}