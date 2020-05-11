namespace Infrastructure.Rest
{
	public class RestResponse
	{
		public bool IsSuccessful { get; }
		public string? ErrorMessage { get; }
		public RestResponse(bool isSuccessful, string? errorMessage)
		{
			IsSuccessful = isSuccessful;
			ErrorMessage = errorMessage;
		}
	}
}