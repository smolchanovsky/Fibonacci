using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Messaging.RabbitMQ
{
	public class RabbitSubscriber<T> : ISubscriber<T> where T : class, IMessage
	{
		private readonly IBusFactory busFactory;
		private readonly ILogger<RabbitSubscriber<T>> logger;
		private readonly ILogger<SubscriptionResult> subscriptionLogger;

		public RabbitSubscriber(
			IBusFactory busFactory,
			ILogger<RabbitSubscriber<T>> logger,
			ILogger<SubscriptionResult> subscriptionLogger)
		{
			this.busFactory = busFactory;
			this.logger = logger;
			this.subscriptionLogger = subscriptionLogger;
		}

		public ISubscriptionResult Subscribe(string subscriptionId, string topic, Func<T, Task> onMessage)
		{
			logger.LogDebug("Start creating subscription {subscriptionId} to topic {topic}", subscriptionId, topic);
			var bus = busFactory.Get();
			var easyNetQSubscription = bus.SubscribeAsync(subscriptionId, onMessage, config => config.WithTopic(topic));
			logger.LogDebug("End creating subscription {subscriptionId} to topic {topic}", subscriptionId, topic);
			return new SubscriptionResult(bus, easyNetQSubscription, subscriptionLogger);
		}
	}
}