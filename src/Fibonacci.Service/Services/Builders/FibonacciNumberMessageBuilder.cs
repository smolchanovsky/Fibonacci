using Fibonacci.Messages;
using Fibonacci.Models;
using Infrastructure.Helpers;

namespace Fibonacci.Service.Services.Builders
{
	public interface IFibonacciNumberMessageBuilder
	{
		FibonacciNumberMessage Build(FibonacciNumber number);
	}

	public class FibonacciNumberMessageBuilder : IFibonacciNumberMessageBuilder
	{
		private readonly IDateTimeManager dateTimeManager;
		private readonly IGuidManager guidManager;

		public FibonacciNumberMessageBuilder(
			IDateTimeManager dateTimeManager,
			IGuidManager guidManager)
		{
			this.dateTimeManager = dateTimeManager;
			this.guidManager = guidManager;
		}
		
		public FibonacciNumberMessage Build(FibonacciNumber number)
		{
			return new FibonacciNumberMessage
			(
				id: guidManager.GetNew(),
				createdAt: dateTimeManager.GetNow(),
				data: new FibonacciNumber
				(
					index: number.Index, 
					value: number.Value
				)
			);
		}
	}
}