using NServiceBus;
using System;
using System.Collections.Generic;
using Shared.Models;

namespace Shared.Responses
{
	[Serializable]
	public class GetCarsResponse : IMessage
	{
		public GetCarsResponse()
		{
			DataId = Guid.NewGuid();
		}

		public Guid DataId { get; set; }
		public List<Car> Cars { get; set; }
	}
}

