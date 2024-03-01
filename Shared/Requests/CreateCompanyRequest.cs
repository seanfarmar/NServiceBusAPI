using System;
using NServiceBus;
using Shared.Models;

namespace Shared.Requests
{

	[Serializable]
	public class CreateCompanyRequest : Request
	{
		public CreateCompanyRequest(Company company)
		{
			DataId = Guid.NewGuid();
			Company = company;
		}
		public Company Company { get; set; }
	}
}
