namespace Infrastructure.Messaging
{
	public interface IBusConfig
	{
		string ConnectionString { get; }
	}
}