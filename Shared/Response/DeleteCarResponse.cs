using NServiceBus;
using System;

namespace Shared.Responses
{
	[Serializable]
	public class DeleteCarResponse : Response
	{
		public DeleteCarResponse()
		{
			DataId = Guid.NewGuid();
		}
	}
}

