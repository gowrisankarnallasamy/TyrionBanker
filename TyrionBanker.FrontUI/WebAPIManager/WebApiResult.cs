namespace TyrionBanker.FrontUI.WebAPIManager
{
	public class WebApiResult<T>
	{
		public enum WebApiStatus
		{
			Success,
			Error,
		}

		public WebApiStatus Status { get; set; }

		public string ErrorMessage { get; set; }

		public T Result { get; set; }
	}
}
