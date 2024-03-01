using System;
using NServiceBus;

namespace Shared.Requests
{
	[Serializable]
	public class DeleteCarRequest : Request
  {
		public DeleteCarRequest(Guid id)
		{
			DataId = Guid.NewGuid();
			CarId = id;
		}
		public Guid CarId;
	}
}
