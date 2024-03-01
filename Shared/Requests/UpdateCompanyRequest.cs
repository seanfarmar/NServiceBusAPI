using System;
using NServiceBus;
using Shared.Models;

namespace Shared.Requests
{
	[Serializable]
	public class UpdateCompanyRequest : Request
  {
		public UpdateCompanyRequest(Company company)
		{
			DataId = Guid.NewGuid();
			Company = company;
		}
		public Company Company;
	}
}
