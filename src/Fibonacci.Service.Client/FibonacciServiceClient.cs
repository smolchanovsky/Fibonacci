using System;
using System.IO;
using System.Threading.Tasks;
using Fibonacci.Models;
using Fibonacci.Service.Client.Exceptions;
using Infrastructure.Rest;
using Microsoft.Extensions.Logging;

namespace Fibonacci.Service.Client
{
	public interface IFibonacciServiceClient
	{
		Task StartComputingNextNumberAsync(Guid clientId, FibonacciNumber fibonacciNumber);
	}

	public class FibonacciServiceClient : IFibonacciServiceClient
	{
		private readonly IRestClient restClient;
		private readonly ILogger<FibonacciServiceClient> logger;
		
		public FibonacciServiceClient(
			IFibonacciServiceClientConfig config,
			IRestClientFactory restClientFactory,
			ILogger<FibonacciServiceClient> logger)
		{
			this.logger = logger;
			
			var restClientConfig = new RestClientConfig(baseUri: config.BaseUri);
			restClient = restClientFactory.Create(restClientConfig);
		}

		public async Task StartComputingNextNumberAsync(Guid clientId, FibonacciNumber fibonacciNumber)
		{
			var resource = Path.Combine("fibonacciSequences", clientId.ToString(), "numbers");
			var request = new RestRequest<FibonacciNumber>(resource, fibonacciNumber);

			// TODO: Implement retry policy
			var response = await restClient.PostAsync(request).ConfigureAwait(false);
			if (!response.IsSuccessful)
			{
				logger.LogError("Start computing next number error: {errorMessage}", response.ErrorMessage);
				throw new FibonacciServiceClientException(response.ErrorMessage!);
			}
		}
	}
}