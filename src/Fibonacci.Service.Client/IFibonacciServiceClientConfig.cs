using System;

namespace Fibonacci.Service.Client
{
	public interface IFibonacciServiceClientConfig
	{
		Uri BaseUri { get; }
	}
}