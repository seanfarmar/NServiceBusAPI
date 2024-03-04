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
	public class CreateCompanyRequestHandler : IHandleMessages<CreateCompanyRequest>
	{
    readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
    readonly CarApiContext _carApiContext;
    readonly ICompanyRepository _companyRepository;
    public CreateCompanyRequestHandler()
    {
      _dbContextOptionsBuilder = new DbContextOptionsBuilder<CarApiContext>();
      _carApiContext = new CarApiContext(_dbContextOptionsBuilder.Options);
      _companyRepository = new CompanyRepository(_carApiContext);
    }

    static ILog log = LogManager.GetLogger<CreateCompanyRequest>();

    public async Task Handle(CreateCompanyRequest message, IMessageHandlerContext context)
    {
      log.Info("Received CreateCompanyRequest.");

      await _companyRepository.AddCompanyAsync(message.Company);

      var response = new CreateCompanyResponse()
      {
        DataId = message.DataId,
        Company = message.Company
      };

      await context.Reply(response);
    }
  }
}