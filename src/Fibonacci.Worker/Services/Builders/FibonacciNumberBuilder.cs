using Fibonacci.Models;

namespace Fibonacci.Worker.Services.Builders
{
	public interface IFibonacciNumberBuilder
	{
		FibonacciNumber BuildFirst();
	}

	public class FibonacciNumberBuilder : IFibonacciNumberBuilder
	{
		public FibonacciNumber BuildFirst()
		{
			return new FibonacciNumber
			(
				index: 1,
				value: 1
			);
		}
	}
}