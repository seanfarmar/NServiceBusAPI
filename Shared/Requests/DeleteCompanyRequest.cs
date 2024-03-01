using System;
using NServiceBus;

namespace Shared.Requests
{
	[Serializable]
	public class DeleteCompanyRequest : Request
  {
		public DeleteCompanyRequest(Guid id)
		{
			DataId = Guid.NewGuid();
			CompanyId = id;
		}
		public Guid CompanyId;
	}
}
