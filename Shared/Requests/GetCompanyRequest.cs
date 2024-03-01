using System;
using NServiceBus;

namespace Shared.Requests
{
	[Serializable]
	public class GetCompanyRequest : Request
  {
		public GetCompanyRequest(Guid id)
		{
			DataId = Guid.NewGuid();
			CompanyId = id;
		}
		public Guid CompanyId { get; set; }
	}
}
