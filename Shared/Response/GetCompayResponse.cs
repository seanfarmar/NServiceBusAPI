using NServiceBus;
using System;

using Shared.Models;

namespace Shared.Responses
{
	[Serializable]
	public class GetCompanyResponse : Response
	{
		public GetCompanyResponse(Guid companyId)
		{
			DataId = Guid.NewGuid();
		}
		public Company Company { get; set; }
	}
}

