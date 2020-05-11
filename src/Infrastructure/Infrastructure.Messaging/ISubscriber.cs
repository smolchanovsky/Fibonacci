using System;
using System.Threading.Tasks;

namespace Infrastructure.Messaging
{
	public interface ISubscriber<T> where T : class, IMessage
	{
		ISubscriptionResult Subscribe(string subscriptionId, string topic, Func<T, Task> onMessage);
	}
}