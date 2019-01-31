using AccountStatistics.Infrastructure.Domains;
using System;
using System.Collections.Generic;

namespace AccountStatistics.Infrastructure.Services.Interfaces
{
	/// <summary>
	/// Сервис для работы с социальной сетью
	/// </summary>
	public interface ISocialNetworkService
	{
		/// <summary>
		/// Авторизоваться в социальной сети
		/// </summary>
		/// <param name="login">Логин учетной записи</param>
		/// <param name="password">Пароль учетной записи</param>
		/// <param name="inputCaptcha">Метод, реализующий ввод капчи
		/// (принимает URL, возвращает распознанный текст)</param>
		void Authorize(string login, string password, Func<string, string> inputCaptcha = null);

		/// <summary>
		/// Получить последние посты заданной учетной записи или группы
		/// </summary>
		/// <param name="authorId">Идентификатор или короткое имя учетной записи или группы</param>
		/// <param name="postsCount">Количество постов</param>
		/// <returns>Список последних постов, либо null, если учетная запись или группы с таким идентификатором не найдена</returns>
		List<PostDomain> GetLastPosts(string authorId, ulong postsCount);

		/// <summary>
		/// Отправить пост от имени текущей учетной записи
		/// </summary>
		/// <param name="postText">Текст поста</param>
		void SendPost(string postText);
	}
}