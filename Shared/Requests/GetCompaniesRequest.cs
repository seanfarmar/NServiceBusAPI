using System;
using NServiceBus;

namespace Shared.Requests
{
	[Serializable]
	public class GetCompaniesRequest : Request
  {
		public GetCompaniesRequest()
		{
			DataId = Guid.NewGuid();
		}
	}
}
