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
	public class DeleteCompanyRequestHandler : IHandleMessages<DeleteCompanyRequest>
	{
    readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
    readonly CarApiContext _carApiContext;
    readonly ICompanyRepository _companyRepository;
    public DeleteCompanyRequestHandler()
    {
      _dbContextOptionsBuilder = new DbContextOptionsBuilder<CarApiContext>();
      _carApiContext = new CarApiContext(_dbContextOptionsBuilder.Options);
      _companyRepository = new CompanyRepository(_carApiContext);
    }

    static ILog log = LogManager.GetLogger<GetCompanyRequest>();

    public async Task Handle(DeleteCompanyRequest message, IMessageHandlerContext context)
    {
      log.Info("Received DeleteCompanyRequest.");

      await _companyRepository.RemoveCompanyAsync(message.CompanyId);

      var response = new DeleteCompanyResponse()
      {
        DataId = message.DataId,
      };

      await context.Reply(response);
    }
  }
}