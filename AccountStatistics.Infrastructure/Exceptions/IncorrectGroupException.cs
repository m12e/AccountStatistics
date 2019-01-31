using System;

namespace AccountStatistics.Infrastructure.Exceptions
{
	/// <summary>
	/// Исключение, которое создается, если был указан некорректный идентификатор группы
	/// </summary>
	public class IncorrectGroupException : AccountStatisticsException
	{
		private const string EXCEPTION_MESSAGE = "Указан некорректный идентификатор группы: ";

		public IncorrectGroupException(string accountId, Exception innerException)
			: base(EXCEPTION_MESSAGE, accountId, innerException)
		{
		}
	}
}