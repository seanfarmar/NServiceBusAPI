using NServiceBus;
using System;

using Shared.Models;

namespace Shared.Responses
{
	[Serializable]
	public class CreateCompanyResponse : Response
	{
		public CreateCompanyResponse()
		{
			DataId = Guid.NewGuid();
		}
		public Company Company { get; set; }
	}
}

