using NServiceBus;
using System;
using System.Collections.Generic;
using Shared.Models;

namespace Shared.Responses
{
	[Serializable]
	public class GetCarsResponse : Response
	{
		public GetCarsResponse()
		{
			DataId = Guid.NewGuid();
		}
		public List<Car> Cars { get; set; }
	}
}

