using Microsoft.Extensions.Configuration;

namespace Fibonacci.Worker.Configs
{
	public interface IWorkerConfig
	{
		int ComputedSequencesCount { get; }
	}

	public class WorkerConfig : IWorkerConfig
	{
		private readonly IConfiguration configuration;

		public WorkerConfig(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		public int ComputedSequencesCount => configuration.GetSection("CalculatedSequences:Count").Get<int>();
	}
}