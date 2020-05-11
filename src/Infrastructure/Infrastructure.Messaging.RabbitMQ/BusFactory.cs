using EasyNetQ;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Messaging.RabbitMQ
{
	public interface IBusFactory
	{
		IBus Get();
	}

	public class BusFactory : IBusFactory
	{
		private readonly object sync = new object();
		
		private readonly IBusConfig busConfig;
		private readonly ILogger<RabbitBus> logger;
		private volatile IBus? bus;

		public BusFactory(IBusConfig busConfig, ILogger<RabbitBus> logger)
		{
			this.busConfig = busConfig;
			this.logger = logger;
		}
		
		public IBus Get()
		{
			if (bus != null)
				return bus;

			lock(sync)
			{
				return bus ??= RabbitHutch
					.CreateBus(busConfig.ConnectionString, registerServices => registerServices.Register(factory => logger));
			}
		}
	}
}