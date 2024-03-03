using System;
using System.Linq;
using System.Threading.Tasks;
using NServiceBus;
using Shared.Requests;
using Shared.Responses;
using Shared.Models;

namespace Client.Utils
{
	public class Utils
    {
	    public static async Task<GetCompaniesResponse> GetCompaniesResponseAsync(IEndpointInstance endpointInstance)
	    {
		    var message = new GetCompaniesRequest();
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var responseTask = await endpointInstance
			    .Request<GetCompaniesResponse>(message, sendOptions);
		    return responseTask;
	    }

	    public static async Task<GetCompanyResponse> GetCompanyResponseAsync(Guid id, IEndpointInstance endpointInstance)
	    {
		    var message = new GetCompanyRequest(id);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var responseTask = await endpointInstance
          .Request<GetCompanyResponse>(message, sendOptions);
		    return responseTask;
	    }


	    public static async Task<CreateCarResponse> CreateCarResponseAsync(Car car, IEndpointInstance endpointInstance)
	    {
		    var message = new CreateCarRequest(car);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var responseTask = await endpointInstance
          .Request<CreateCarResponse>(message, sendOptions);
		    return responseTask;
	    }

	    public static async Task<GetCarResponse> GetCarResponseAsync(Guid carId, IEndpointInstance endpointInstance)
	    {
		    var message = new GetCarRequest(carId);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var responseTask = await endpointInstance
          .Request<GetCarResponse>(message, sendOptions);
		    return responseTask;
	    }

	    public static async Task<DeleteCarResponse> DeleteCarResponseAsync(Guid carId, IEndpointInstance endpointInstance)
		{
		    var message = new DeleteCarRequest(carId);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var responseTask = await endpointInstance
          .Request<DeleteCarResponse>(message, sendOptions);
		    return responseTask;
	    }

	    public static async Task<UpdateCarResponse> UpdateCarResponseAsync(Car car, IEndpointInstance endpointInstance)
		{
		    var message = new UpdateCarRequest(car);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var responseTask = await endpointInstance
          .Request<UpdateCarResponse>(message, sendOptions);
		    return responseTask;
	    }

	    public static async Task<GetCarsResponse> GetCarsResponseAsync(IEndpointInstance endpointInstance)
	    {
			try
			{
				var message = new GetCarsRequest();
				var sendOptions = new SendOptions();
				sendOptions.SetDestination("NServiceBusCore.Server");
				var responseTask = await endpointInstance
          .Request<GetCarsResponse>(message, sendOptions);
				return responseTask;
			}catch (Exception ex)
			{
				var a = ex;
				throw;
			}
	    }
	    public async Task<bool> CompanyExistsAsync(Guid id, IEndpointInstance endpointInstance)
		{
		    var getCompaniesResponse = await GetCompaniesResponseAsync(endpointInstance);
		    var companies = getCompaniesResponse.Companies;
		    return companies.Any(e => e.Id == id);
	    }

	    public static Task<CreateCompanyResponse> CreateCompanyResponseAsync(Company company, IEndpointInstance endpointInstance)
	    {
		    var message = new CreateCompanyRequest(company);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var responseTask = endpointInstance
			    .Request<CreateCompanyResponse>(message, sendOptions);
		    return responseTask;
	    }

	    public static Task<DeleteCompanyResponse> DeleteCompanyResponseAsync(Guid id, IEndpointInstance endpointInstance)

		{
		    var message = new DeleteCompanyRequest(id);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var responseTask = endpointInstance
			    .Request<DeleteCompanyResponse>(message, sendOptions);
		    return responseTask;
	    }

	    public static Task<UpdateCompanyResponse> UpdateCompanyResponseAsync(Company company, IEndpointInstance endpointInstance)

		{
		    var message = new UpdateCompanyRequest(company);
		    var sendOptions = new SendOptions();
		    sendOptions.SetDestination("NServiceBusCore.Server");
		    var responseTask = endpointInstance
			    .Request<UpdateCompanyResponse>(message, sendOptions);
		    return responseTask;
	    }
	}
}
