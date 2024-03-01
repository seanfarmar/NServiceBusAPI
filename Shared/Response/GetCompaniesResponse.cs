using NServiceBus;
using System;
using System.Collections.Generic;
using Shared.Models;

namespace Shared.Responses
{
	[Serializable]
	public class GetCompaniesResponse : Response
	{
		public GetCompaniesResponse()
		{
			DataId = Guid.NewGuid();
		}
		public List<Company> Companies { get; set; }
	}
}

