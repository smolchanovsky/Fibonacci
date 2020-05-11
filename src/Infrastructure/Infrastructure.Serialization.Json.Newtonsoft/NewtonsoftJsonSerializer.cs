using Newtonsoft.Json;

namespace Infrastructure.Serialization.Json.Newtonsoft
{
	public class NewtonsoftJsonSerializer : IJsonSerializer
	{
		public string Serialize(object obj)
		{
			return JsonConvert.SerializeObject(obj);
		}
	}
}