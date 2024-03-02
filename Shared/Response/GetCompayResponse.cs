using NServiceBus;
using System;

using Shared.Models;

namespace Shared.Responses
{
	[Serializable]
	public class GetCompanyResponse : IMessage
	{
		public GetCompanyResponse(Guid companyId)
		{
			DataId = Guid.NewGuid();
		}

    public Guid DataId { get; set; }
    public Company Company { get; set; }
	}
}

