using ES.Net.Exceptions;

namespace ES.Net
{
	public class ElasticsearchServerError
	{
		public int Status { get; set; }
		public string Error { get; set; }
		public string ExceptionType { get; set; }

		internal static ElasticsearchServerError Create(OneToOneServerException e)
		{
			if (e == null) return null;
			return new ElasticsearchServerError
			{
				Status = e.status,
				Error = e.error
			};
		}
	}
}