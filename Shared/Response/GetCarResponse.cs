using NServiceBus;
using System;
using Shared.Models;

namespace Shared.Responses
{
	[Serializable]
	public class GetCarResponse : Response
	{
		public GetCarResponse()
		{
			DataId = Guid.NewGuid();
		}
		public Car Car { get; set; }
	}
}

