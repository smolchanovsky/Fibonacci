using Fibonacci.Algorithms;
using Fibonacci.Messages;
using Fibonacci.Service.Configs;
using Fibonacci.Service.Services;
using Fibonacci.Service.Services.Builders;
using Fibonacci.Validation;
using Infrastructure.BackgroundJob;
using Infrastructure.BackgroundJob.Hangfire;
using Infrastructure.Helpers;
using Infrastructure.Messaging;
using Infrastructure.Messaging.RabbitMQ;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fibonacci.Service
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
				.ConfigureServices((hostContext, services) =>
				{
					services.AddLogging(opt =>
					{
						opt.AddConsole(c =>
						{
							c.TimestampFormat = "[HH:mm:ss] ";
						});
					});
					
					services.AddControllers().AddNewtonsoftJson();
					
					services.AddSingleton<IFibonacciNumberService, FibonacciNumberService>();
					services.AddSingleton<IFibonacciNumberMessageBuilder, FibonacciNumberMessageBuilder>();
					
					services.AddSingleton<IFibonacciNumberValidator, FibonacciNumberValidator>();
					services.AddSingleton<IFibonacciNumberProvider, FibonacciNumberProvider>();
					services.AddSingleton<IFibonacciAlgorithm, RecursiveFibonacciAlgorithm>();
					
					services.AddSingleton<IBusConfig, BusConfig>();
					services.AddSingleton<IBusFactory, BusFactory>();
					services.AddSingleton<IPublisher<FibonacciNumberMessage>, RabbitPublisher<FibonacciNumberMessage>>();
					services.AddSingleton<IBackgroundJobDirector, HangfireBackgroundJobDirector>();
					
					services.AddSingleton<IGuidManager, GuidManager>();
					services.AddSingleton<IDateTimeManager, DateTimeManager>();
				});
	}
}