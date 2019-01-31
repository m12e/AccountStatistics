using AccountStatistics.Infrastructure.Services;
using AccountStatistics.Infrastructure.Services.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;

namespace AccountStatistics.Tests
{
	[TestFixture]
	public class AccountStatisticsTests
	{
		[Test]
		public void JsonSerializationServiceTest()
		{
			ISerializationService serializationService = new JsonSerializationService();

			var data = new Dictionary<char, double>
			{
				['а'] = 0.5,
				['б'] = 0.25,
				['в'] = 0.333
			};
			var serializedData = serializationService.SerializeData(data);

			const string expectedResult = "{\"а\":0.5,\"б\":0.25,\"в\":0.333}";
			Assert.That(serializedData == expectedResult);
		}

		[Test]
		public void LetterFrequencyServiceTest()
		{
			ILetterFrequencyService letterFrequencyService = new LetterFrequencyService();

			const string text = "а1б2в3 аб_АwZ!";
			var letterFrequency = letterFrequencyService.GetLetterFrequency(text, 3);

			var expectedResult = new Dictionary<char, double>
			{
				['а'] = 0.375,
				['б'] = 0.25,
				['в'] = 0.125,
				['w'] = 0.125,
				['z'] = 0.125
			};

			Assert.That(letterFrequency.Count == expectedResult.Count);
			foreach (var letter in expectedResult.Keys)
			{
				Assert.That(letterFrequency.ContainsKey(letter));
				Assert.That(letterFrequency[letter] == expectedResult[letter]);
			}
		}
	}
}