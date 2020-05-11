using System;
using System.Threading.Tasks;
using Fibonacci.Algorithms;
using Fibonacci.Messages;
using Fibonacci.Models;
using Fibonacci.Service.Services.Builders;
using Infrastructure.Messaging;
using Microsoft.Extensions.Logging;

namespace Fibonacci.Service.Services
{
	public interface IFibonacciNumberService
	{
		Task PublishNextAsync(Guid sequenceId, FibonacciNumber number);
	}

	public class FibonacciNumberService : IFibonacciNumberService
	{
		private readonly IPublisher<FibonacciNumberMessage> messagePublisher;
		private readonly IFibonacciNumberMessageBuilder messageBuilder;
		private readonly IFibonacciNumberProvider numberProvider;
		private readonly ILogger<FibonacciNumberService> logger;

		public FibonacciNumberService(
			IPublisher<FibonacciNumberMessage> messagePublisher,
			IFibonacciNumberMessageBuilder messageBuilder,
			IFibonacciNumberProvider numberProvider,
			ILogger<FibonacciNumberService> logger)
		{
			this.messagePublisher = messagePublisher;
			this.messageBuilder = messageBuilder;
			this.numberProvider = numberProvider;
			this.logger = logger;
		}

		public async Task PublishNextAsync(Guid sequenceId, FibonacciNumber number)
		{
			var nextNumber = numberProvider.GetNext(number);
			logger.LogInformation("Next number for sequence {sequenceId} is {value}", sequenceId, nextNumber.Value);
			
			var message = messageBuilder.Build(nextNumber);
			await messagePublisher.PublishAsync(topic: sequenceId.ToString(), message).ConfigureAwait(false);
			logger.LogInformation("Message {messageId} with nex number for sequence {sequenceId} is published", message.Id, sequenceId);
		}
	}
}