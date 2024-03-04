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
	public class UpdateCompanyRequestHandler : IHandleMessages<UpdateCompanyRequest>
	{
    readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
    readonly CarApiContext _carApiContext;
    readonly ICompanyRepository _companyRepository;
    public UpdateCompanyRequestHandler()
    {
      _dbContextOptionsBuilder = new DbContextOptionsBuilder<CarApiContext>();
      _carApiContext = new CarApiContext(_dbContextOptionsBuilder.Options);
      _companyRepository = new CompanyRepository(_carApiContext);
    }

    static ILog log = LogManager.GetLogger<UpdateCompanyRequest>();

		public async Task Handle(UpdateCompanyRequest message, IMessageHandlerContext context)
		{
			log.Info("Received UpdateCompanyRequest");

      await _companyRepository.UpdateCompanyAsync(message.Company);

      var response = new UpdateCompanyResponse()
      {
        DataId = message.DataId,
        Company = message.Company
      };

      await context.Reply(response);

    }
	}
}