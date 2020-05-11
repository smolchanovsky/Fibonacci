using System;
using System.Threading.Tasks;
using Fibonacci.Messages;
using Fibonacci.Models;
using Fibonacci.Service.Client;
using Infrastructure.Messaging;
using Microsoft.Extensions.Logging;

namespace Fibonacci.Worker.Services
{
	public interface IFibonacciRemoteComputingProvider
	{
		Task StartComputingNextNumberAsync(Guid sequenceId, FibonacciNumber number);
		ISubscriptionResult SubscribeToResult(Guid sequenceId, Func<Guid, FibonacciNumberMessage, Task> onMessage);
	}

	public class FibonacciRemoteComputingProvider : IFibonacciRemoteComputingProvider
	{
		private readonly IFibonacciServiceClient fibonacciServiceClient;
		private readonly ISubscriber<FibonacciNumberMessage> messageSubscriber;
		private readonly ILogger<FibonacciRemoteComputingProvider> logger;

		public FibonacciRemoteComputingProvider(
			IFibonacciServiceClient fibonacciServiceClient,
			ISubscriber<FibonacciNumberMessage> messageSubscriber,
			ILogger<FibonacciRemoteComputingProvider> logger)
		{
			this.fibonacciServiceClient = fibonacciServiceClient;
			this.messageSubscriber = messageSubscriber;
			this.logger = logger;
		}
		
		public async Task StartComputingNextNumberAsync(Guid sequenceId, FibonacciNumber number)
		{
			logger.LogInformation("Start remote computing next number");
			await fibonacciServiceClient.StartComputingNextNumberAsync(sequenceId, number).ConfigureAwait(false);
		}

		public ISubscriptionResult SubscribeToResult(Guid sequenceId, Func<Guid, FibonacciNumberMessage, Task> onMessage)
		{
			logger.LogInformation("Start subscribe to remote computing result messages");
			return messageSubscriber
				.Subscribe(
					subscriptionId: sequenceId.ToString(), 
					topic: sequenceId.ToString(), 
					onMessage: message => onMessage(sequenceId, message));
		}
	}
}