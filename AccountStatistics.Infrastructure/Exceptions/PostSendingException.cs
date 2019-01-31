using System;

namespace AccountStatistics.Infrastructure.Exceptions
{
	/// <summary>
	/// Исключение, которое создается при ошибке отправки поста
	/// </summary>
	public class PostSendingException : AccountStatisticsException
	{
		private const string EXCEPTION_MESSAGE = "Ошибка отправки поста адресату с идентификатором: ";

		public PostSendingException(string groupId)
			: base(EXCEPTION_MESSAGE, groupId)
		{
		}

		public PostSendingException(string groupId, Exception innerException)
			: base(EXCEPTION_MESSAGE, groupId, innerException)
		{
		}
	}
}