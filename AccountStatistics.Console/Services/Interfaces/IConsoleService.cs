using System;

namespace AccountStatistics.Console.Services.Interfaces
{
	/// <summary>
	/// Сервис для работы с консолью
	/// </summary>
	public interface IConsoleService
	{
		/// <summary>
		/// Запустить цикл обработки ввода
		/// </summary>
		void Run();

		/// <summary>
		/// Показать сообщение об ошибке
		/// </summary>
		void PrintErrorMessage(Exception exception);
	}
}