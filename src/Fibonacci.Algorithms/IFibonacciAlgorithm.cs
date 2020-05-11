using System.Numerics;

namespace Fibonacci.Algorithms
{
	public interface IFibonacciAlgorithm
	{
		BigInteger Get(BigInteger n);
	}
}