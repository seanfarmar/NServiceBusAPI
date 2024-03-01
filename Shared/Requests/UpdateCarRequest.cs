using System;
using Shared.Models;

namespace Shared.Requests
{
	[Serializable]
	public class UpdateCarRequest : Request
  {
		public UpdateCarRequest(Car car)
		{
			DataId = Guid.NewGuid();
			Car = car;
		}
		public Car Car { get; set; }
	}
}
