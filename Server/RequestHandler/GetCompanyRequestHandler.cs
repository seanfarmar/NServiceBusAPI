using NServiceBus;
using System.Threading.Tasks;
using NServiceBus.Logging;
using Shared.Requests;
using System;
using Shared.Responses;
using Server.DAL;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;
using System.Linq;

namespace Server.Requesthandler
{
	public class GetCompanyRequestHandler : IHandleMessages<GetCompanyRequest>
	{
		readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
		public GetCompanyRequestHandler()
		{
			_dbContextOptionsBuilder = new DbContextOptionsBuilder<CarApiContext>();
		}

		static ILog log = LogManager.GetLogger<GetCompanyRequest>();

		public Task Handle(GetCompanyRequest message, IMessageHandlerContext context)
		{
			log.Info("Received GetCompanyRequest");

			Company company;
			using (var unitOfWork = new CarUnitOfWork(new CarApiContext(_dbContextOptionsBuilder.Options)))
      {
        var list = unitOfWork.Companies.GetAll();
        company = list.Where(c => c.Id == message.CompanyId).SingleOrDefault();
      }

      var response = new GetCompanyResponse(message.CompanyId)
			{
				DataId = Guid.NewGuid(),
				Company = company
			};

			var reply = context.Reply(response);
			return reply;

		}
	}
}