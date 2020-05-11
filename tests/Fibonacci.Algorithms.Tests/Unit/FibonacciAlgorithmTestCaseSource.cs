using System;
using System.Collections;
using System.Numerics;
using NUnit.Framework;

namespace Fibonacci.Algorithms.Tests.Unit
{
	public class FibonacciAlgorithmTestCaseSource
	{
		public static IEnumerable ValidCases
		{
			get
			{
				yield return new TestCaseData((BigInteger)0, (BigInteger)0)
					.SetDescription("Zero fibonacci number");
				yield return new TestCaseData((BigInteger)1, (BigInteger)1)
					.SetDescription("First fibonacci number");
				yield return new TestCaseData((BigInteger)2, (BigInteger)1)
					.SetDescription("Second fibonacci number");
				yield return new TestCaseData((BigInteger)5, (BigInteger)5)
					.SetDescription("Fifth fibonacci number");
				yield return new TestCaseData((BigInteger)10, (BigInteger)55)
					.SetDescription("Tenth fibonacci number");
				yield return new TestCaseData((BigInteger)33, (BigInteger)3524578)
					.SetDescription("Thirty-third fibonacci number");
			}
		}
		
		public static IEnumerable ArgumentExceptionCases
		{
			get
			{
				yield return new TestCaseData((BigInteger)(-1))
					.SetDescription("Negative number");
				yield return new TestCaseData((BigInteger)Int64.MinValue)
					.SetDescription("Int64.MinValue");
			}
		}
	}
}