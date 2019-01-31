using System;

namespace AccountStatistics.Infrastructure.Exceptions
{
	/// <summary>
	/// Абстрактный базовый класс для исключений проекта
	/// </summary>
	public abstract class AccountStatisticsException : Exception
	{
		/// <summary>
		/// Текст, который ставится в начале и в конце строкового параметра для его выделения
		/// </summary>
		private const string PARAMETER_QUOTE_MARK = "'";

		protected AccountStatisticsException()
		{
		}

		protected AccountStatisticsException(string message)
			: base(message)
		{
		}

		protected AccountStatisticsException(string message, string parameter)
			: base(message + QuoteParameter(parameter))
		{
		}

		protected AccountStatisticsException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		protected AccountStatisticsException(string message, string parameter, Exception innerException)
			: base(message + QuoteParameter(parameter), innerException)
		{
		}

		/// <summary>
		/// Поставить значение заданного строкового параметра в кавычки
		/// </summary>
		protected static string QuoteParameter(string parameter)
		{
			return PARAMETER_QUOTE_MARK + parameter + PARAMETER_QUOTE_MARK;
		}
	}
}