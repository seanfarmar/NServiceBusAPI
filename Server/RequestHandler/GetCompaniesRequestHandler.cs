using NServiceBus;
using System.Threading.Tasks;
using NServiceBus.Logging;
using Shared.Requests;
using Shared.Responses;
using System.Linq;
using Server.Data;
using Microsoft.EntityFrameworkCore;
using Server.DAL;

namespace Server.Requesthandler
{
	public class GetCompaniesRequestHandler : IHandleMessages<GetCompaniesRequest>
	{
    readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
    readonly CarApiContext _carApiContext;
    readonly ICompanyRepository _companyRepository;
    public GetCompaniesRequestHandler()
    {
      _dbContextOptionsBuilder = new DbContextOptionsBuilder<CarApiContext>();
      _carApiContext = new CarApiContext(_dbContextOptionsBuilder.Options);
      _companyRepository = new CompanyRepository(_carApiContext);
    }

    static ILog log = LogManager.GetLogger<GetCompaniesRequest>();

		public async Task Handle(GetCompaniesRequest message, IMessageHandlerContext context)
		{
			log.Info("Received GetCompaniesRequest");

      var companies = await _companyRepository.GetAllCompaniesAsync();

      var response = new GetCompaniesResponse()
      {
        DataId = message.DataId,
        Companies = companies.ToList()
      };

      await context.Reply(response);
    }
	}
}