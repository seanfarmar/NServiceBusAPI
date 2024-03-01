using System.Linq;
using Shared.Requests;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;
using Shared.Responses;
using Server.DAL;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;

namespace Server.Requesthandler
{
	public class GetCarsRequestHandler : IHandleMessages<GetCarsRequest>
	{
		readonly DbContextOptionsBuilder<CarApiContext> _dbContextOptionsBuilder;
		public GetCarsRequestHandler()
		{
			_dbContextOptionsBuilder = new DbContextOptionsBuilder<CarApiContext>();
		}

		static ILog log = LogManager.GetLogger<GetCarsRequestHandler>();

		public Task Handle(GetCarsRequest message, IMessageHandlerContext context)
		{
			log.Info("Received GetCarsRequest.");
			List<Car> cars;
			using (var unitOfWork = new CarUnitOfWork(new CarApiContext(_dbContextOptionsBuilder.Options)))
			{
				cars = unitOfWork.Cars.GetAll().ToList();
			}
			var response = new GetCarsResponse()
			{
				Cars = cars
			};

			var reply = context.Reply(response);
			return reply;
		}
	}
}