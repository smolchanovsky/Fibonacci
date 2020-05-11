namespace Infrastructure.Rest
{
	public interface IRestClientFactory
	{
		IRestClient Create(IRestClientConfig config);
	}
}