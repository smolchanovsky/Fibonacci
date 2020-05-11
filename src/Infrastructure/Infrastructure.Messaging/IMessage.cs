using System;

namespace Infrastructure.Messaging
{
	public interface IMessage
	{
		public Guid Id { get; }
		DateTime CreatedAt { get; }
	}
	
	public interface IMessage<T> : IMessage where T : notnull
	{
		T Data { get; }
	}
}