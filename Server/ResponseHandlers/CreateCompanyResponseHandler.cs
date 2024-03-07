using NServiceBus;
using System.Threading.Tasks;
using NServiceBus.Logging;
using Shared.Responses;
using Shared.Responses;
using Server.Data;
using Microsoft.EntityFrameworkCore;
using Server.DAL;

namespace Server.ResponseHandlers
{
	public class CreateCompanyResponseHandler : IHandleMessages<CreateCompanyResponse>
	{
    readonly ICompanyRepository _companyRepository;

    public CreateCompanyResponseHandler(ICompanyRepository companyRepository)
    {
      _companyRepository = companyRepository;
    }

    static ILog log = LogManager.GetLogger<CreateCompanyResponse>();

    public async Task Handle(CreateCompanyResponse message, IMessageHandlerContext context)
    {
      log.Info("Received CreateCompanyResponse.");

      //await _companyRepository.AddCompanyAsync(message.Company);

      //var response = new CreateCompanyResponse()
      //{
      //  DataId = message.DataId,
      //  Company = message.Company
      //};

      //await context.Reply(response);
    }
  }
}