using NServiceBus;
using System.Threading.Tasks;
using NServiceBus.Logging;
using Shared.Requests;
using Shared.Responses;
using Server.Data;
using Microsoft.EntityFrameworkCore;
using Server.DAL;

namespace Server.Requesthandler
{
	public class GetCompanyRequestHandler : IHandleMessages<GetCompanyRequest>
	{
    readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
    readonly CarApiContext _carApiContext;
    readonly ICompanyRepository _companyRepository;
    public GetCompanyRequestHandler()
    {
      _dbContextOptionsBuilder = new DbContextOptionsBuilder<CarApiContext>();
      _carApiContext = new CarApiContext(_dbContextOptionsBuilder.Options);
      _companyRepository = new CompanyRepository(_carApiContext);
    }

    static ILog log = LogManager.GetLogger<GetCompanyRequest>();

		public async Task Handle(GetCompanyRequest message, IMessageHandlerContext context)
		{
			log.Info("Received GetCompanyRequest");

      var company = await _companyRepository.GetCompanyAsync(message.CompanyId);

      var response = new GetCompanyResponse()
      {
        DataId = message.DataId,
        Company = company
      };

      await context.Reply(response);
    }
	}
}