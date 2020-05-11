using System;

namespace Fibonacci.Service.Client.Exceptions
{
	public class FibonacciServiceClientException : Exception
	{
		public FibonacciServiceClientException()
		{
		}

		public FibonacciServiceClientException(string message) : base(message)
		{
		}

		public FibonacciServiceClientException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}