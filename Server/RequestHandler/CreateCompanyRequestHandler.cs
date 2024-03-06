using Microsoft.Extensions.Logging;
using NServiceBus;
using Server.Data;
using Shared.Requests;
using Shared.Responses;
using System.Threading.Tasks;

namespace Server.Requesthandler
{
    public class CreateCompanyRequestHandler : IHandleMessages<CreateCompanyRequest>
    {
        readonly ICompanyRepository _companyRepository;
        readonly ILogger<CreateCompanyRequestHandler> _logger;

        public CreateCompanyRequestHandler(
            ILogger<CreateCompanyRequestHandler> logger,
            ICompanyRepository companyRepository)
        {
            _logger = logger;
            _companyRepository = companyRepository;
        }

        public async Task Handle(CreateCompanyRequest message, IMessageHandlerContext context)
        {
            _logger.LogInformation("Received CreateCompanyRequest.");

            await _companyRepository.AddCompanyAsync(message.Company)
                .ConfigureAwait(false);

            var response = new CreateCompanyResponse()
            {
                DataId = message.DataId,
                Company = message.Company
            };

            await context.Reply(response)
                .ConfigureAwait(false);
        }
    }
}