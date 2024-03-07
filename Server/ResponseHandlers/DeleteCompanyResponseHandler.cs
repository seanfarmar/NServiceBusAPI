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
	public class DeleteCompanyResponseHandler : IHandleMessages<DeleteCompanyResponse>
	{
    readonly ICompanyRepository _companyRepository;

    public DeleteCompanyResponseHandler(ICompanyRepository caompanyRepository)
    {
      _companyRepository = caompanyRepository;
    }

    static ILog log = LogManager.GetLogger<GetCompanyResponse>();

    public async Task Handle(DeleteCompanyResponse message, IMessageHandlerContext context)
    {
      log.Info("Received DeleteCompanyResponse.");

      //await _companyRepository.RemoveCompanyAsync(message.CompanyId);

      //var response = new DeleteCompanyResponse()
      //{
      //  DataId = message.DataId,
      //};

      //await context.Reply(response);
    }
  }
}