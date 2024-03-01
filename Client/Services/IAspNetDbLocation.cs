using System.Threading.Tasks;
using Shared.Responses;

namespace Client.Services
{
	using NServiceBus;

	public interface IAspNetDbLocation
	{
		Task<GetAspNetDbResponse> GetAspNetDbAsync(IEndpointInstance endpointInstance);

	}
}
