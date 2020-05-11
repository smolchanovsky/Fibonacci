using System.Net.Http;
using Infrastructure.Serialization.Json;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Rest.HttpClient
{
	public class HttpClientRestClientFactory : IRestClientFactory
	{
		private readonly IJsonSerializer jsonSerializer;
		private readonly IHttpClientFactory httpClientFactory;
		private readonly ILogger<HttpClientRestClient> logger;

		public HttpClientRestClientFactory(
			IJsonSerializer jsonSerializer,
			IHttpClientFactory httpClientFactory,
			ILogger<HttpClientRestClient> logger)
		{
			this.jsonSerializer = jsonSerializer;
			this.httpClientFactory = httpClientFactory;
			this.logger = logger;
		}
		
		public IRestClient Create(IRestClientConfig config)
		{
			return new HttpClientRestClient(config, jsonSerializer, httpClientFactory, logger);
		}
	}
}