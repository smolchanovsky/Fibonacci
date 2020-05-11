using Infrastructure.Messaging;
using Microsoft.Extensions.Configuration;

namespace Fibonacci.Worker.Configs
{
	public class BusConfig : IBusConfig
	{
		private readonly IConfiguration configuration;

		public BusConfig(IConfiguration configuration)
		{
			this.configuration = configuration;
		}
		
		public string ConnectionString => configuration["Bus:ConnectionString"];
	}
}