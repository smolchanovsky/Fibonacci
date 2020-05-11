using System;
using Fibonacci.Service.Client;
using Microsoft.Extensions.Configuration;

namespace Fibonacci.Worker.Configs
{
	public class FibonacciServiceClientConfig : IFibonacciServiceClientConfig
	{
		private readonly IConfiguration configuration;

		public FibonacciServiceClientConfig(IConfiguration configuration)
		{
			this.configuration = configuration;
		}
		
		public Uri BaseUri => new Uri(configuration["FibonacciService:BaseUri"]);
	}
}