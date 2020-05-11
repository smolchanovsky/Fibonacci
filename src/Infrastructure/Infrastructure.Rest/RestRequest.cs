namespace Infrastructure.Rest
{
	public class RestRequest<T> where T : notnull
	{
		public string Resource { get; }
		public T Content { get; }
		
		public RestRequest(string resource, T content)
		{
			Resource = resource;
			Content = content;
		}
	}
}