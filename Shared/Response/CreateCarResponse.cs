using NServiceBus;
using System;
using Shared.Models;

namespace Shared.Responses
{
	[Serializable]
	public class CreateCarResponse : Response
	{
		public CreateCarResponse()
		{
			DataId = Guid.NewGuid();
		}
		public Car Car { get; set; }
	}
}

