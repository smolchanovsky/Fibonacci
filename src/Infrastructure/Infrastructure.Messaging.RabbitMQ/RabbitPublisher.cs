using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Messaging.RabbitMQ
{
	public class RabbitPublisher<T> : IPublisher<T> where T : class, IMessage
	{
		private readonly IBusFactory busFactory;
		private readonly ILogger<RabbitPublisher<T>> logger;

		public RabbitPublisher(
			IBusFactory busFactory,
			ILogger<RabbitPublisher<T>> logger)
		{
			this.busFactory = busFactory;
			this.logger = logger;
		}

		public async Task PublishAsync(string topic, T message)
		{
			logger.LogDebug("Start publishing message {messageId} to topic {topic}", message.Id, topic);
			await busFactory.Get().PublishAsync(message, topic).ConfigureAwait(false);
			logger.LogDebug("End publishing message {messageId} to topic {topic}", message.Id, topic);
		}
	}
}