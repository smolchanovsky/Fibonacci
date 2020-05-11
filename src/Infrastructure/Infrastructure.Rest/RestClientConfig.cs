using System;

namespace Infrastructure.Rest
{
	public interface IRestClientConfig
	{
		Uri BaseUri { get; }
	}

	public class RestClientConfig : IRestClientConfig
	{
		public Uri BaseUri { get; }
		
		public RestClientConfig(Uri baseUri)
		{
			BaseUri = baseUri;
		}
	}
}