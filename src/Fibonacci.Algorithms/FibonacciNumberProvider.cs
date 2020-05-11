using Fibonacci.Models;
using Fibonacci.Validation;

namespace Fibonacci.Algorithms
{
	public interface IFibonacciNumberProvider
	{
		FibonacciNumber GetNext(FibonacciNumber number);
	}

	public class FibonacciNumberProvider : IFibonacciNumberProvider
	{
		private readonly IFibonacciNumberValidator numberValidator;
		private readonly IFibonacciAlgorithm fibonacciAlgorithm;

		public FibonacciNumberProvider(
			IFibonacciNumberValidator numberValidator,
			IFibonacciAlgorithm fibonacciAlgorithm)
		{
			this.numberValidator = numberValidator;
			this.fibonacciAlgorithm = fibonacciAlgorithm;
		}
		
		public FibonacciNumber GetNext(FibonacciNumber number)
		{
			numberValidator.EnsureValid(number);
			
			return new FibonacciNumber
			(
				index: number.Index + 1,
				value: fibonacciAlgorithm.Get(number.Index - 1) + number.Value
			);
		}
	}
}