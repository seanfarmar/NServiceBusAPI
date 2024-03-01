using NServiceBus;
using System.Threading.Tasks;
using NServiceBus.Logging;
using Shared.Requests;
using System;
using Shared.Responses;
using System.Linq;
using Server.DAL;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;
namespace Server.Requesthandler
{
	public class GetCompaniesRequestHandler : IHandleMessages<GetCompaniesRequest>
	{
		readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
    public GetCompaniesRequestHandler()
    {
      _dbContextOptionsBuilder = new DbContextOptionsBuilder<CarApiContext>();
    }

    static ILog log = LogManager.GetLogger<GetCompaniesRequest>();

		public Task Handle(GetCompaniesRequest message, IMessageHandlerContext context)
		{
			log.Info("Received GetCompaniesRequest");

			List<Company> companies;
			using (var unitOfWork = new CarUnitOfWork(new CarApiContext(_dbContextOptionsBuilder.Options)))
			{
				companies = unitOfWork.Companies.GetAll().ToList();
			}

			var response = new GetCompaniesResponse
			{
				DataId = Guid.NewGuid(),
				Companies = companies
			};

			var reply = context.Reply(response);
			return reply;

		}
	}
}