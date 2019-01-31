using System;

namespace AccountStatistics.Infrastructure.Exceptions
{
	/// <summary>
	/// Исключение, которое создается при ошибке авторизации
	/// </summary>
	public class AuthorizationException : AccountStatisticsException
	{
		private const string EXCEPTION_MESSAGE = "Ошибка авторизации по логину учетной записи: ";

		public AuthorizationException(string login)
			: base(EXCEPTION_MESSAGE, login)
		{
		}

		public AuthorizationException(string login, Exception innerException)
			: base(EXCEPTION_MESSAGE, login, innerException)
		{
		}
	}
}