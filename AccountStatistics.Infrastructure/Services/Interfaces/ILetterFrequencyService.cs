using System.Collections.Generic;

namespace AccountStatistics.Infrastructure.Services.Interfaces
{
	/// <summary>
	/// Сервис для вычисления частотности букв в тексте
	/// </summary>
	public interface ILetterFrequencyService
	{
		/// <summary>
		/// Получить частотность букв в заданном тексте
		/// </summary>
		/// <param name="fractionalDigitsCount">Количество цифр в дробной части числа</param>
		/// <result>Словарь, где ключом является буква, а значением — ее частотность</result>
		Dictionary<char, double> GetLetterFrequency(string text, int fractionalDigitsCount);
	}
}