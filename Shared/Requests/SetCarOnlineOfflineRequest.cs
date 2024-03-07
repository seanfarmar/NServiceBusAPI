using System;
using Shared.Models;

namespace Shared.Requests
{
	[Serializable]
	public class SetCarOnlineOfflineRequest
	{
		public SetCarOnlineOfflineRequest(Car car)
		{
			DataId = Guid.NewGuid();
			Car = car;
		}
		public Guid DataId { get; set; }
		public Car Car { get; set; }
	}
}