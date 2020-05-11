using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fibonacci.Worker.Configs;
using Fibonacci.Worker.Services;
using Infrastructure.Helpers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MoreLinq;

namespace Fibonacci.Worker
{
	public class FibonacciComputingWorker : BackgroundService
	{
		private readonly IWorkerConfig workerConfig;
		private readonly IFibonacciComputingRunner fibonacciComputingRunner;
		private readonly IGuidManager guidManager;
		private readonly ILogger<FibonacciComputingWorker> logger;

		public FibonacciComputingWorker(
			IWorkerConfig workerConfig,
			IFibonacciComputingRunner fibonacciComputingRunner,
			IGuidManager guidManager,
			ILogger<FibonacciComputingWorker> logger)
		{
			this.workerConfig = workerConfig;
			this.fibonacciComputingRunner = fibonacciComputingRunner;
			this.guidManager = guidManager;
			this.logger = logger;
		}

		protected override Task ExecuteAsync(CancellationToken stoppingToken)
		{
			logger.LogInformation("Start worker");
			
			var subscribingResults = Enumerable
				.Range(0, workerConfig.ComputedSequencesCount)
				.Select(_ => guidManager.GetNew())
				.Select(fibonacciComputingRunner.RunComputing)
				.ToArray();
			
			stoppingToken.WaitHandle.WaitOne();
			logger.LogInformation("Cancellation has been requested");
			
			subscribingResults.ForEach(x => x.Dispose());
			logger.LogInformation("All subscriptions are canceled");
			
			return Task.FromResult(0);
		}
	}
}
