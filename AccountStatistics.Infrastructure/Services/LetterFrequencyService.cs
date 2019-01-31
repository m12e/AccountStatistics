using System;
using System.Collections.Generic;
using System.Linq;
using AccountStatistics.Infrastructure.Services.Interfaces;

namespace AccountStatistics.Infrastructure.Services
{
	public class LetterFrequencyService : ILetterFrequencyService
	{
		public Dictionary<char, double> GetLetterFrequency(string text, int fractionalDigitsCount)
		{
			var letters = text
				.ToCharArray()
				.Where(char.IsLetter)
				.Select(char.ToLower)
				.ToList();

			var totalLettersCount = letters.Count;
			var letterCount = new Dictionary<char, int>();
			var letterFrequency = new Dictionary<char, double>();

			foreach (var letter in letters)
			{
				if (letterCount.ContainsKey(letter))
					letterCount[letter]++;
				else
					letterCount[letter] = 1;
			}

			foreach (var letter in letterCount.Keys.OrderBy(key => key))
			{
				var frequency = (double)letterCount[letter] / totalLettersCount;
				letterFrequency.Add(letter, Math.Round(frequency, fractionalDigitsCount));
			}
			return letterFrequency;
		}
	}
}