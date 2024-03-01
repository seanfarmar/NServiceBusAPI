using System;
using NServiceBus;

namespace Shared.Requests
{
	[Serializable]
	public class GetCarRequest : Request
  {
		public GetCarRequest(Guid id)
		{
			DataId = Guid.NewGuid();
			CarId = id;
		}
		public Guid CarId { get; set; }
  }
}
