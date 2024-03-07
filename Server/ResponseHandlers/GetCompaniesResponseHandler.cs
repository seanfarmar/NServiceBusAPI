using NServiceBus;
using System.Threading.Tasks;
using NServiceBus.Logging;
using Shared.Responses;
using Shared.Responses;
using System.Linq;
using Server.Data;
using Microsoft.EntityFrameworkCore;
using Server.DAL;

namespace Server.ResponseHandlers
{
	public class GetCompaniesResponseHandler : IHandleMessages<GetCompaniesResponse>
	{
    readonly ICompanyRepository _companyRepository;

    public GetCompaniesResponseHandler(ICompanyRepository caompanyRepository)
    {
      _companyRepository = caompanyRepository;
    }

    static ILog log = LogManager.GetLogger<GetCompaniesResponse>();

		public async Task Handle(GetCompaniesResponse message, IMessageHandlerContext context)
		{
			log.Info("Received GetCompaniesResponse");

      var companies = await _companyRepository.GetAllCompaniesAsync();

      //var response = new GetCompaniesResponse()
      //{
      //  DataId = message.DataId,
      //  Companies = companies.ToList()
      //};

      //await context.Reply(response);
    }
	}
}