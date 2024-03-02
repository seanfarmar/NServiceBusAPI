using NServiceBus;
using System;
using System.Collections.Generic;
using Shared.Models;

namespace Shared.Responses
{
	[Serializable]
	public class GetCompaniesResponse : IMessage
	{
		public GetCompaniesResponse()
		{
			DataId = Guid.NewGuid();
		}

    public Guid DataId { get; set; }
    public List<Company> Companies { get; set; }
	}
}

