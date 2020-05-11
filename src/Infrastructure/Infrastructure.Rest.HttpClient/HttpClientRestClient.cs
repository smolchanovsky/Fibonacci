using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Serialization.Json;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Rest.HttpClient
{
	public class HttpClientRestClient : IRestClient
	{
		private readonly IRestClientConfig config;
		private readonly IJsonSerializer jsonSerializer;
		private readonly IHttpClientFactory httpClientFactory;
		private readonly ILogger<HttpClientRestClient> logger;

		public HttpClientRestClient(
			IRestClientConfig config,
			IJsonSerializer jsonSerializer,
			IHttpClientFactory httpClientFactory,
			ILogger<HttpClientRestClient> logger)
		{
			this.config = config;
			this.jsonSerializer = jsonSerializer;
			this.httpClientFactory = httpClientFactory;
			this.logger = logger;
		}
		
		public async Task<RestResponse> PostAsync<T>(RestRequest<T> request) where T : notnull
		{
			var jsonContent = jsonSerializer.Serialize(request.Content);
			var httpRequest = new HttpRequestMessage(HttpMethod.Post, new Uri(config.BaseUri, request.Resource))
			{
				Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
			};
			
			var httpClient = httpClientFactory.CreateClient(nameof(HttpClientRestClient));
			var httpResponse = await httpClient.SendAsync(httpRequest).ConfigureAwait(false);
			if (!httpResponse.IsSuccessStatusCode)
				logger.LogWarning("Request failed - {statusCode}: {reasonPhrase}", httpResponse.StatusCode, httpResponse.ReasonPhrase);
			
			return new RestResponse
			(
				httpResponse.IsSuccessStatusCode,
				errorMessage: httpResponse.IsSuccessStatusCode ? null : $"Request failed - {httpResponse.StatusCode}: {httpResponse.ReasonPhrase}"
			);
		}
	}
}