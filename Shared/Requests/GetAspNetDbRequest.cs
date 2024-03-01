using System;
using NServiceBus;

namespace Shared.Requests
{
	[Serializable]
	public class GetAspNetDbRequest : Request
  {
		public GetAspNetDbRequest()
		{
			DataId = Guid.NewGuid();
		}
	}
}
