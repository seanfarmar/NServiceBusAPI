using NServiceBus;
using System;

namespace Shared.Responses
{
	[Serializable]
	public class DeleteCompanyResponse : Response
	{
		public DeleteCompanyResponse()
		{
			DataId = Guid.NewGuid();
		}
		public Guid DataId { get; set; }
	}
}

