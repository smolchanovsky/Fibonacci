using System.Numerics;

namespace Fibonacci.Models
{
	public class FibonacciNumber
	{
		public BigInteger Value { get; }
		public BigInteger Index { get; }
		
		public FibonacciNumber(BigInteger value, BigInteger index)
		{
			Value = value;
			Index = index;
		}
	}
}