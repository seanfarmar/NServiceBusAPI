using System;
using NServiceBus;
using Shared.Models;

namespace Shared.Requests
{

	[Serializable]
	public class CreateCarRequest : Request
	{
		public CreateCarRequest(Car car)
		{
			DataId = Guid.NewGuid();
			Car = car;
		}
		public Car Car { get; set; }
	}
}
