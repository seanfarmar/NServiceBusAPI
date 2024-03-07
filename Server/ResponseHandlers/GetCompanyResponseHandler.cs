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
	public class GetCompanyResponseHandler : IHandleMessages<GetCompanyResponse>
	{
    readonly ICompanyRepository _companyRepository;

    public GetCompanyResponseHandler(ICompanyRepository caompanyRepository)
    {
      _companyRepository = caompanyRepository;
    }

    static ILog log = LogManager.GetLogger<GetCompanyResponse>();

		public async Task Handle(GetCompanyResponse message, IMessageHandlerContext context)
		{
			log.Info("Received GetCompanyResponse");

      //var company = await _companyRepository.GetCompanyAsync(message.CompanyId);

      //var response = new GetCompanyResponse()
      //{
      //  DataId = message.DataId,
      //  Company = company
      //};

      //await context.Reply(response);
    }
	}
}