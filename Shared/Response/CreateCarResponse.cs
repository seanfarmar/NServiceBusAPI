using NServiceBus;
using System;
using Shared.Models;

namespace Shared.Responses
{
	[Serializable]
	public class CreateCarResponse : IMessage
	{
		public CreateCarResponse()
		{
			DataId = Guid.NewGuid();
		}
    public Guid DataId { get; set; }
    public Car Car { get; set; }
	}
}

