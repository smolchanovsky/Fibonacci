using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using HangfireBackgroundJob = Hangfire.BackgroundJob;

namespace Infrastructure.BackgroundJob.Hangfire
{
	public class HangfireBackgroundJobDirector : IBackgroundJobDirector
	{
		private readonly ILogger<HangfireBackgroundJobDirector> logger;

		public HangfireBackgroundJobDirector(ILogger<HangfireBackgroundJobDirector> logger)
		{
			this.logger = logger;
		}
		
		public string Enqueue(Expression<Func<Task>> methodCall)
		{
			logger.LogDebug("Start background job");
			return HangfireBackgroundJob.Enqueue(methodCall);
		}
	}
}