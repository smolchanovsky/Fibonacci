using System.Threading.Tasks;

namespace Infrastructure.Rest
{
	public interface IRestClient
	{
		Task<RestResponse> PostAsync<T>(RestRequest<T> request) where T : notnull;
	}
}