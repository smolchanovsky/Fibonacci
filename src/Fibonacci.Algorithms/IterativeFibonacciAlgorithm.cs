using System;
using System.Numerics;

namespace Fibonacci.Algorithms
{
	public class IterativeFibonacciAlgorithm : IFibonacciAlgorithm
	{
		public BigInteger Get(BigInteger n)
		{
			if (n < 0)
				throw new ArgumentException("The value cannot be negative.", nameof(n));
			
			if (n == 0)
				return 0;
			if (n == 1 || n == 2)
				return 1;
			
			BigInteger left = 1;
			BigInteger right = 1;
			BigInteger current = 0;
			
			for (BigInteger i = 3; i <= n; i++) 
			{
				current = left + right;
				right = left;
				left = current;
			}
			
			return current;
		}
	}
}