using NServiceBus;
using System;

namespace Shared.Responses
{
	[Serializable]
	public class GetAspNetDbResponse : IMessage
	{
		public GetAspNetDbResponse()
		{
			DataId = Guid.NewGuid();
		}

    public Guid DataId { get; set; }
		public string AspNetDb { get; set; } = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\AspNet.db";


    public static implicit operator string(GetAspNetDbResponse v)
		{
			throw new NotImplementedException();
		}
	}
}

