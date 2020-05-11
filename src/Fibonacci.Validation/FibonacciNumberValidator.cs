using System;
using Fibonacci.Models;

namespace Fibonacci.Validation
{
	public interface IFibonacciNumberValidator
	{
		void EnsureValid(FibonacciNumber number);
	}

	public class FibonacciNumberValidator : IFibonacciNumberValidator
	{
		public void EnsureValid(FibonacciNumber number)
		{
			if (number is null)
				throw new ArgumentNullException(nameof(number));
			if (number.Value < 1)
				throw new ArgumentException("Value must be greater than zero.", nameof(number.Value));
			if (number.Index < 1)
				throw new ArgumentException("Value must be greater than zero.", nameof(number.Index));
		}
	}
}