using NServiceBus;
using System;

namespace Shared.Responses
{
	using Shared.Models;

	[Serializable]
	public class UpdateCompanyResponse : Response
	{
		public UpdateCompanyResponse()
		{
			DataId = Guid.NewGuid();
		}
		public Company Company { get; set; }
	}
}

