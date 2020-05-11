using EasyNetQ;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Messaging.RabbitMQ
{
	public class SubscriptionResult : ISubscriptionResult
	{
		private readonly IBus bus;
		private readonly EasyNetQ.ISubscriptionResult easyNetQSubscription;
		private readonly ILogger<SubscriptionResult> logger;

		public SubscriptionResult(
			IBus bus, 
			EasyNetQ.ISubscriptionResult easyNetQSubscription,
			ILogger<SubscriptionResult> logger)
		{
			this.bus = bus;
			this.easyNetQSubscription = easyNetQSubscription;
			this.logger = logger;
		}
		
		public void Dispose()
		{
			easyNetQSubscription.Dispose();
			bus.Dispose();
			logger.LogDebug("Subscription on queue {queueName} is over", easyNetQSubscription.Queue.Name);
		}
	}
}