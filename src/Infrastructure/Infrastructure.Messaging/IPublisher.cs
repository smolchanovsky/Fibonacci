using System.Threading.Tasks;

namespace Infrastructure.Messaging
{
	public interface IPublisher<T> where T : class, IMessage
	{
		Task PublishAsync(string topic, T message);
	}
}