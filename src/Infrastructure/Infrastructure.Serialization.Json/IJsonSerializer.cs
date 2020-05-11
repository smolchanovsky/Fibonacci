namespace Infrastructure.Serialization.Json
{
	public interface IJsonSerializer
	{
		string Serialize(object obj);
	}
}