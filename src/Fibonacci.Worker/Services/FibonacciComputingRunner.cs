using System;
using System.Threading.Tasks;
using Fibonacci.Algorithms;
using Fibonacci.Messages;
using Fibonacci.Worker.Services.Builders;
using Infrastructure.Messaging;
using Microsoft.Extensions.Logging;

namespace Fibonacci.Worker.Services
{
	public interface IFibonacciComputingRunner
	{
		ISubscriptionResult RunComputing(Guid sequenceId);
	}

	public class FibonacciComputingRunner : IFibonacciComputingRunner
	{
		private readonly IFibonacciRemoteComputingProvider fibonacciRemoteComputingProvider;
		private readonly IFibonacciLocalComputingProvider fibonacciLocalComputingProvider;
		private readonly IFibonacciNumberBuilder numberBuilder;
		private readonly ILogger<FibonacciComputingRunner> logger;

		public FibonacciComputingRunner(
			IFibonacciRemoteComputingProvider fibonacciRemoteComputingProvider,
			IFibonacciLocalComputingProvider fibonacciLocalComputingProvider, 
			IFibonacciNumberBuilder numberBuilder,
			ILogger<FibonacciComputingRunner> logger)
		{
			this.fibonacciRemoteComputingProvider = fibonacciRemoteComputingProvider;
			this.fibonacciLocalComputingProvider = fibonacciLocalComputingProvider;
			this.numberBuilder = numberBuilder;
			this.logger = logger;
		}
		
		public ISubscriptionResult RunComputing(Guid sequenceId)
		{
			fibonacciRemoteComputingProvider.StartComputingNextNumberAsync(sequenceId, numberBuilder.BuildFirst());
			return fibonacciRemoteComputingProvider.SubscribeToResult(sequenceId, OnFinishRemoteComputing);
		}

		private async Task OnFinishRemoteComputing(Guid sequenceId, FibonacciNumberMessage numberMessage)
		{
			logger.LogInformation("Message {numberMessageId} with number {numberMessageValue} is received", numberMessage.Id, numberMessage.Data.Value);
			
			var nextNumber = fibonacciLocalComputingProvider.ComputingNextNumber(numberMessage.Data);
			logger.LogInformation("Next number is calculated - {nextNumberValue}", nextNumber.Value);
			
			await fibonacciRemoteComputingProvider.StartComputingNextNumberAsync(sequenceId, nextNumber).ConfigureAwait(false);
		}
	}
}