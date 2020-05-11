using System;
using System.Numerics;
using FluentAssertions;
using NUnit.Framework;

namespace Fibonacci.Algorithms.Tests.Unit
{
	[TestFixture]
	public class RecursiveFibonacciAlgorithmTest
	{
		private IFibonacciAlgorithm fibonacciAlgorithm;

		[OneTimeSetUp]
		public void OneTimeSetUp()
		{
			fibonacciAlgorithm = new RecursiveFibonacciAlgorithm();
		}

		[TestCaseSource(typeof(FibonacciAlgorithmTestCaseSource), nameof(FibonacciAlgorithmTestCaseSource.ValidCases))]
		public void Get_ValidArgument(BigInteger n, BigInteger expectedResult)
		{
			var actualResult = fibonacciAlgorithm.Get(n);

			actualResult.Should().Be(expectedResult);
		}
		
		[TestCaseSource(typeof(FibonacciAlgorithmTestCaseSource), nameof(FibonacciAlgorithmTestCaseSource.ArgumentExceptionCases))]
		public void Get_InvalidArgument_ArgumentException(BigInteger n)
		{
			fibonacciAlgorithm
				.Invoking(x => x.Get(n))
				.Should()
				.ThrowExactly<ArgumentException>();
		}
	}
}