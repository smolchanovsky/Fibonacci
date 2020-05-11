using System;
using System.Collections;
using Fibonacci.Algorithms;
using Fibonacci.Messages;
using Fibonacci.Models;
using Fibonacci.Service.Services;
using Fibonacci.Service.Services.Builders;
using Fibonacci.Validation;
using FluentAssertions;
using FluentAssertions.Execution;
using Infrastructure.Helpers;
using Infrastructure.Messaging;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Fibonacci.Service.Tests.Integration
{
	[TestFixture]
	public class FibonacciNumberServiceTest
	{
		private static readonly Guid testGuidValue = Guid.Parse("20703bc8-49ae-4ba7-8c7a-832ef47eedf3");
		private static readonly DateTime testDateTimeValue = DateTime.Parse("5/11/2020 10:02:56 PM");
		
		private Mock<IPublisher<FibonacciNumberMessage>> publisherMoq;
		private FibonacciNumberService service;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			publisherMoq = new Mock<IPublisher<FibonacciNumberMessage>>();
				
			var guidManager = new Mock<IGuidManager>();
			guidManager.Setup(x => x.GetNew()).Returns(testGuidValue);
			var dateTimeManager = new Mock<IDateTimeManager>();
			dateTimeManager.Setup(x => x.GetNow()).Returns(testDateTimeValue);
			var messageBuilder = new FibonacciNumberMessageBuilder(dateTimeManager.Object, guidManager.Object);
			
			var numberValidator = new FibonacciNumberValidator(); 
			var algorithm = new IterativeFibonacciAlgorithm();
			var numberProvider = new FibonacciNumberProvider(numberValidator, algorithm);
			
			var loggerMoq = new Mock<ILogger<FibonacciNumberService>>();
			
			service = new FibonacciNumberService(publisherMoq.Object, messageBuilder, numberProvider, loggerMoq.Object);
		}

		[TestCaseSource(nameof(ValidCases))]
		public void PublishNextAsync_ValidCase(Guid sequenceId, FibonacciNumber number, string expectedTopic, FibonacciNumberMessage expectedMessage)
		{
			string actualTopic = null;
			FibonacciNumberMessage actualMessage = null;
			publisherMoq
				.Setup(x => x.PublishAsync(It.IsAny<string>(), It.IsAny<FibonacciNumberMessage>()))
				.Callback<string, FibonacciNumberMessage>((topic, message) =>
				{
					actualTopic = topic;
					actualMessage = message;
				});

			service.PublishNextAsync(sequenceId, number).GetAwaiter().GetResult();

			using (new AssertionScope())
			{
				actualTopic.Should().Be(expectedTopic);
				actualMessage.Should().BeEquivalentTo(expectedMessage);
			}
		}

		[TestCaseSource(nameof(ArgumentExceptionCases))]
		public void PublishNextAsync_NotPositiveNumber_ArgumentException(Guid sequenceId, FibonacciNumber number)
		{
			service
				.Invoking(x => x.PublishNextAsync(sequenceId, number).GetAwaiter().GetResult())
				.Should()
				.ThrowExactly<ArgumentException>();
		}
		
		[Test]
		public void PublishNextAsync_NullNumber_ArgumentNullException()
		{
			service
				.Invoking(x => x.PublishNextAsync(testGuidValue, null).GetAwaiter().GetResult())
				.Should()
				.ThrowExactly<ArgumentNullException>();
		}

		public static IEnumerable ValidCases
		{
			get
			{
				yield return new TestCaseData(
						testGuidValue, 
						new FibonacciNumber (index: 1, value: 1),
						testGuidValue.ToString(),
						new FibonacciNumberMessage (id: testGuidValue, createdAt: testDateTimeValue, data: new FibonacciNumber (index: 2, value: 1)))
					.SetDescription("First fibonacci number");
				yield return new TestCaseData(
						testGuidValue, 
						new FibonacciNumber (index: 2, value: 1),
						testGuidValue.ToString(),
						new FibonacciNumberMessage (id: testGuidValue, createdAt: testDateTimeValue, data: new FibonacciNumber (index: 3, value: 2)))
					.SetDescription("Second fibonacci number");
				yield return new TestCaseData(
						testGuidValue, 
						new FibonacciNumber (index: 10, value:5),
						testGuidValue.ToString(),
						new FibonacciNumberMessage (id: testGuidValue, createdAt: testDateTimeValue, data: new FibonacciNumber (index: 11, value: 89)))
					.SetDescription("Tenth fibonacci number");
			}
		}
		
		public static IEnumerable ArgumentExceptionCases
		{
			get
			{
				yield return new TestCaseData(testGuidValue, new FibonacciNumber (index: Int64.MinValue, value: 1))
					.SetDescription("Index is Int64.MinValue");
				yield return new TestCaseData(testGuidValue, new FibonacciNumber (index: 1, value: Int64.MinValue))
					.SetDescription("Value is Int64.MinValue");
				yield return new TestCaseData(testGuidValue, new FibonacciNumber (index: -1, value: 1))
					.SetDescription("Index is negative number");
				yield return new TestCaseData(testGuidValue, new FibonacciNumber (index: 1, value: -1))
					.SetDescription("Value is negative number");
				yield return new TestCaseData(testGuidValue, new FibonacciNumber (index: 0, value: 1))
					.SetDescription("Index is zero");
				yield return new TestCaseData(testGuidValue, new FibonacciNumber (index: 1, value: 0))
					.SetDescription("Value is zero");
			}
		}
	}
}