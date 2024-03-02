using System;
using Shared.Models;
using NServiceBus;

namespace Shared.Requests
{
	[Serializable]
	public class UpdateCarRequest : IMessage
  {
		public UpdateCarRequest(Car car)
		{
			DataId = Guid.NewGuid();
			Car = car;
		}

    public Guid DataId { get; set; }
    public Car Car { get; set; }
	}
}
