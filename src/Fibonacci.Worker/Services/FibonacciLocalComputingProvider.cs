using Fibonacci.Algorithms;
using Fibonacci.Models;
using Microsoft.Extensions.Logging;

namespace Fibonacci.Worker.Services
{
	public interface IFibonacciLocalComputingProvider
	{
		FibonacciNumber ComputingNextNumber(FibonacciNumber number);
	}

	public class FibonacciLocalComputingProvider : IFibonacciLocalComputingProvider
	{
		private readonly IFibonacciNumberProvider numberProvider;
		private readonly ILogger<FibonacciLocalComputingProvider> logger;

		public FibonacciLocalComputingProvider(
			IFibonacciNumberProvider numberProvider,
			ILogger<FibonacciLocalComputingProvider> logger)
		{
			this.numberProvider = numberProvider;
			this.logger = logger;
		}
		
		public FibonacciNumber ComputingNextNumber(FibonacciNumber number)
		{
			logger.LogInformation("Start local computing next number");
			var nextNumber = numberProvider.GetNext(number);
			logger.LogInformation("End local computing next number - {nextNumberValue}", nextNumber.Value);
			return nextNumber;
		}
	}
}