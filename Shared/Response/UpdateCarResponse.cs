using NServiceBus;
using System;
using Shared.Models;

namespace Shared.Responses
{
	[Serializable]
	public class UpdateCarResponse : Response
	{
		public UpdateCarResponse()
		{
			DataId = Guid.NewGuid();
		}
		public Car Car { get; set; }
	}
}

