using System;

namespace AccountStatistics.Infrastructure.Exceptions
{
	/// <summary>
	/// Исключение, которое создается при ошибке ввода капчи
	/// </summary>
	public class CaptchaInputException : AccountStatisticsException
	{
		private const string EXCEPTION_MESSAGE = "Ошибка ввода капчи";

		public CaptchaInputException(Exception innerException)
			: base(EXCEPTION_MESSAGE, innerException)
		{
		}
	}
}