using AccountStatistics.Console.Services.Interfaces;
using AccountStatistics.Infrastructure.Services.Interfaces;
using System;
using System.Linq;
using SysConsole = System.Console;

namespace AccountStatistics.Console.Services
{
	public class ConsoleService : IConsoleService
	{
		#region Константы

		/// <summary>
		/// Сообщение, которое появляется при старте программы
		/// </summary>
		private const string GREETING_MESSAGE = "Добро пожаловать в программу 'AccountStatistics'!";

		/// <summary>
		/// Сообщение, которое появляется при необходимости ввести капчу
		/// </summary>
		private const string CAPTCHA_UNPUT_MESSAGE = "Пожалуйста, введите текст с изображения по следующей ссылке:";

		/// <summary>
		/// Сообщение, которое появляется, если не удалось найти учетную запись с указанным идентификатором
		/// </summary>
		private const string ACCOUNT_OR_GROUP_NOT_FOUND_MESSAGE = "Не удалось найти учетную запись или группу с таким идентификатором. Введите другой идентификатор:";

		/// <summary>
		/// Сообщение, которое появляется, если у заданной учетной записи нет постов
		/// </summary>
		private const string NO_POSTS_MESSAGE = "У заданной учетной записи нет постов. Введите другой идентификатор:";

		/// <summary>
		/// Текст, который выводится в сообщении со статистикой
		/// </summary>
		private const string STATISTICS_MESSAGE = ", статистика для последних 5 постов:";

		/// <summary>
		/// Сообщение, которое появляется при выходе из программы
		/// </summary>
		private const string FAREWELL_MESSAGE = "До свидания!";

		/// <summary>
		/// Сообщение, которое появляется при ошибке
		/// </summary>
		private const string ERROR_MESSAGE = "Во время выполнения программы возникла ошибка. Пожалуйста, обратитесь к разработчику.";

		/// <summary>
		/// Сообщение, которое появляется перед текстом ошибки
		/// </summary>
		private const string ERROR_TEXT_MESSAGE = "Описание ошибки:";

		/// <summary>
		/// Сообщение со справкой по программе
		/// </summary>
		private readonly string _helpMessage =
$@"Программа запрашивает идентификаторы учетный записей или групп, пока не будет введена пустая строка.
После каждого корректного идентификатора статистика по последним {Consts.ACCOUNT_POSTS_COUNT} постам автора
выводится в консоль и отображается в новом посте в социальной сети от вашего имени.

Примеры запросов:
'durov', 'id1', '1' - идентификаторы учетной записи Павла Дурова,
'apiclub', 'club1' - идентификаторы группы 'ВКонтакте API'.";

		#endregion

		private readonly ILetterFrequencyService _letterFrequencyService;
		private readonly ISerializationService _serializationService;
		private readonly ISocialNetworkService _socialNetworkService;

		private readonly Func<string, string> _inputCaptcha;

		public ConsoleService(
			ILetterFrequencyService letterFrequencyService,
			ISerializationService serializationService,
			ISocialNetworkService socialNetworkService)
		{
			_letterFrequencyService = letterFrequencyService;
			_serializationService = serializationService;
			_socialNetworkService = socialNetworkService;

			_inputCaptcha = url =>
			{
				SysConsole.WriteLine(CAPTCHA_UNPUT_MESSAGE);
				SysConsole.WriteLine(url);
				return SysConsole.ReadLine();
			};
		}

		public void Run()
		{
			SysConsole.WriteLine(GREETING_MESSAGE);
			SysConsole.WriteLine();
			SysConsole.WriteLine(_helpMessage);
			SysConsole.WriteLine();

			_socialNetworkService.Authorize(
				Consts.ACCOUNT_LOGIN,
				Consts.ACCOUNT_PASSWORD,
				_inputCaptcha);

			while (true)
			{
				var authorId = SysConsole.ReadLine();
				if (string.IsNullOrEmpty(authorId))
					break;

				var lastPosts = _socialNetworkService
					.GetLastPosts(authorId, Consts.ACCOUNT_POSTS_COUNT);

				if (lastPosts == null)
				{
					SysConsole.WriteLine(ACCOUNT_OR_GROUP_NOT_FOUND_MESSAGE);
					continue;
				}
				if (lastPosts.Count == 0)
				{
					SysConsole.WriteLine(NO_POSTS_MESSAGE);
					continue;
				}
				var joinedPosts = string.Join(string.Empty, lastPosts);

				var letterFrequency = _letterFrequencyService
					.GetLetterFrequency(joinedPosts, Consts.FRACTIONAL_DIGITS_COUNT);
				var serializedFrequency = _serializationService
					.SerializeData(letterFrequency);
				var authorName = lastPosts.First().AuthorName;
				var postText = $"{authorName} (id = {authorId})" + STATISTICS_MESSAGE + Environment.NewLine + serializedFrequency;

				_socialNetworkService.SendPost(postText);

				SysConsole.WriteLine(postText);
				SysConsole.WriteLine();
			}

			SysConsole.WriteLine(FAREWELL_MESSAGE);
			SysConsole.ReadKey();
		}

		public void PrintErrorMessage(Exception exception)
		{
			SysConsole.WriteLine(ERROR_MESSAGE);
			SysConsole.WriteLine(ERROR_TEXT_MESSAGE);
			SysConsole.WriteLine(exception.Message);
			SysConsole.ReadKey();
		}
	}
}