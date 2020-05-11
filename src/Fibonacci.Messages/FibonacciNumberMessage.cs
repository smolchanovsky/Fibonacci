using System;
using Fibonacci.Models;
using Infrastructure.Messaging;

namespace Fibonacci.Messages
{
	public class FibonacciNumberMessage : IMessage<FibonacciNumber>
	{
		public Guid Id { get; }
		public DateTime CreatedAt { get; }
		public FibonacciNumber Data { get; }
		
		public FibonacciNumberMessage(Guid id, DateTime createdAt, FibonacciNumber data)
		{
			Id = id;
			CreatedAt = createdAt;
			Data = data;
		}
	}
}