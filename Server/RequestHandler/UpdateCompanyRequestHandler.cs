using NServiceBus;
using System.Threading.Tasks;
using NServiceBus.Logging;
using Shared.Requests;
using System;
using Shared.Responses;
using Server.DAL;
using Microsoft.EntityFrameworkCore;
using Server.Data;

namespace Server.Requesthandler
{
	public class UpdateCompanyRequestHandler : IHandleMessages<UpdateCompanyRequest>
	{
		readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
		public UpdateCompanyRequestHandler()
		{
			_dbContextOptionsBuilder = new DbContextOptionsBuilder<CarApiContext>();
		}
		static ILog log = LogManager.GetLogger<UpdateCompanyRequest>();

		public Task Handle(UpdateCompanyRequest message, IMessageHandlerContext context)
		{
			log.Info("Received UpdateCompanyRequest");
			using (var unitOfWork = new CarUnitOfWork(new CarApiContext(_dbContextOptionsBuilder.Options)))
			{
				var existingCompany = unitOfWork.Companies.Get(message.Company.Id);
				if (existingCompany != null)
				{
					if (!Equals(existingCompany, message.Company))
					{
						existingCompany = message.Company;
            unitOfWork.Companies.Update(message.Company);
						unitOfWork.Complete();
					}
				}
				else
				{
					throw new Exception("Cannot update company, not fund in database");
				}
			}

        var response = new UpdateCompanyResponse()
			{
				DataId = Guid.NewGuid(),
				Company = message.Company
			};

			var reply = context.Reply(response);
			return reply;

		}
	}
}