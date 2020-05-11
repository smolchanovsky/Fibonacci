using System.Net.Http;
using Fibonacci.Algorithms;
using Fibonacci.Messages;
using Fibonacci.Service.Client;
using Fibonacci.Validation;
using Fibonacci.Worker.Configs;
using Fibonacci.Worker.Services;
using Fibonacci.Worker.Services.Builders;
using Infrastructure.Helpers;
using Infrastructure.Messaging;
using Infrastructure.Messaging.RabbitMQ;
using Infrastructure.Rest;
using Infrastructure.Rest.HttpClient;
using Infrastructure.Serialization.Json;
using Infrastructure.Serialization.Json.Newtonsoft;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Fibonacci.Worker
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureServices((hostContext, services) =>
				{
					services.AddHostedService<FibonacciComputingWorker>();
					
					services.AddLogging(opt =>
					{
						opt.AddConsole(consoleLoggerOpts =>
						{
							consoleLoggerOpts.TimestampFormat = "[HH:mm:ss] ";
						});
					});
					
					services.AddHttpClient(nameof(HttpClientRestClient)).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
					{
						ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
					});
					
					services.AddSingleton<IWorkerConfig, WorkerConfig>();
					services.AddSingleton<IFibonacciComputingRunner, FibonacciComputingRunner>();
					services.AddSingleton<IFibonacciNumberBuilder, FibonacciNumberBuilder>();
					services.AddSingleton<IFibonacciLocalComputingProvider, FibonacciLocalComputingProvider>();
					services.AddSingleton<IFibonacciRemoteComputingProvider, FibonacciRemoteComputingProvider>();
					services.AddSingleton<IFibonacciServiceClientConfig, FibonacciServiceClientConfig>();

					services.AddSingleton<IFibonacciNumberValidator, FibonacciNumberValidator>();
					services.AddSingleton<IFibonacciNumberProvider, FibonacciNumberProvider>();
					services.AddSingleton<IFibonacciAlgorithm, RecursiveFibonacciAlgorithm>();
					services.AddSingleton<IFibonacciServiceClient, FibonacciServiceClient>();
					
					services.AddSingleton<IBusConfig, BusConfig>();
					services.AddSingleton<IBusFactory, BusFactory>();
					services.AddSingleton<IRestClientFactory, HttpClientRestClientFactory>();
					services.AddSingleton<ISubscriber<FibonacciNumberMessage>, RabbitSubscriber<FibonacciNumberMessage>>();
					
					services.AddSingleton<IGuidManager, GuidManager>();
					services.AddSingleton<IJsonSerializer, NewtonsoftJsonSerializer>();
				});
	}
}
