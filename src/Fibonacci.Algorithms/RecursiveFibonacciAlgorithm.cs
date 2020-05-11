using System;
using System.Numerics;

namespace Fibonacci.Algorithms
{
	public class RecursiveFibonacciAlgorithm : IFibonacciAlgorithm
	{
		public BigInteger Get(BigInteger n)
		{
			if (n < 0)
				throw new ArgumentException("The value cannot be negative.", nameof(n));

			if (n <= 1)
				return n;
			
			return Get(n - 1) + Get(n - 2);
		}
	}
}