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
	public class UpdateCompanyResponseHandler : IHandleMessages<UpdateCompanyResponse>
	{
    readonly ICompanyRepository _companyRepository;

    public UpdateCompanyResponseHandler(ICompanyRepository caompanyRepository)
    {
      _companyRepository = caompanyRepository;
    }


    static ILog log = LogManager.GetLogger<UpdateCompanyResponse>();

		public async Task Handle(UpdateCompanyResponse message, IMessageHandlerContext context)
		{
			log.Info("Received UpdateCompanyResponse");

      //await _companyRepository.UpdateCompanyAsync(message.Company);

      //var response = new UpdateCompanyResponse()
      //{
      //  DataId = message.DataId,
      //  Company = message.Company
      //};

      //await context.Reply(response);

    }
	}
}