using NServiceBus;
using System;

namespace Shared.Responses
{
	[Serializable]
	public class GetAspNetDbResponse : Response
	{
		public GetAspNetDbResponse()
		{
			DataId = Guid.NewGuid();
		}
		public string AspNetDb { get; set; }

		public static implicit operator string(GetAspNetDbResponse v)
		{
			throw new NotImplementedException();
		}
	}
}

