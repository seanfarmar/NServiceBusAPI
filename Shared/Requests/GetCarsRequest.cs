using System;
using NServiceBus;

namespace Shared.Requests
{
	[Serializable]
	public class GetCarsRequest : Request
  {
		public GetCarsRequest()
		{
			DataId = Guid.NewGuid();
		}
	}
}
