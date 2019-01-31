using System;
using System.Collections.Generic;
using System.Linq;
using AccountStatistics.Infrastructure.Domains;
using AccountStatistics.Infrastructure.Exceptions;
using AccountStatistics.Infrastructure.Services.Interfaces;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Enums.SafetyEnums;
using VkNet.Exception;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace AccountStatistics.Infrastructure.Services
{
	/// <summary>
	/// Сервис для работы с социальной сетью (Вконтакте)
	/// </summary>
	public class VkSocialNetworkService : ISocialNetworkService
	{
		/// <summary>
		/// Идентификатор приложения Вконтакте
		/// </summary>
		private const ulong APPLICATION_ID = 6834516;

		/// <summary>
		/// Идентификатор группы, в которой будут создаваться посты
		/// </summary>
		private const string GROUP_ID = "";

		private static readonly VkApi VkApi = new VkApi();

		public void Authorize(string login, string password, Func<string, string> inputCaptcha = null)
		{
			var apiAuthParams = new ApiAuthParams
			{
				ApplicationId = APPLICATION_ID,
				Login = login,
				Password = password,
				Settings = Settings.All
			};

			while (true)
			{
				try
				{
					VkApi.Authorize(apiAuthParams);
					break;
				}
				catch (CaptchaNeededException e)
				{
					if (inputCaptcha == null)
						throw new CaptchaInputException(new ArgumentNullException(nameof(inputCaptcha)));

					var recognizedCaptcha = inputCaptcha(e.Img.AbsoluteUri);
					apiAuthParams.CaptchaSid = e.Sid;
					apiAuthParams.CaptchaKey = recognizedCaptcha;
				}
				catch (VkApiException e)
				{
					throw new AuthorizationException(login, e);
				}
			}
		}

		public List<PostDomain> GetLastPosts(string authorId, ulong postsCount)
		{
			User user = null;
			Group group = null;
			try
			{
				user = VkApi.Users
					.Get(new List<string> { authorId })
					.First();
			}
			catch (InvalidUserIdException)
			{
				try
				{
					group = VkApi.Groups
						.GetById(null, authorId, null)
						.First();
				}
				catch (VkApiException)
				{
					return null;
				}
			}

			var authorName = user == null ? group.Name : $"{user.FirstName} {user.LastName}";
			var wallGetParams = new WallGetParams
			{
				// Для группы идентификатор сохраняется со знаком '-'
				OwnerId = user?.Id ?? -group.Id,
				Count = postsCount,
				Filter = WallFilter.Owner
			};
			var wallGetObject = VkApi.Wall.Get(wallGetParams);

			return wallGetObject
				.WallPosts
				.Select(post => new PostDomain(authorId, authorName, post.Text))
				.ToList();
		}

		public void SendPost(string postText)
		{
			Group group;
			try
			{
				group = VkApi.Groups
					.GetById(null, GROUP_ID, null)
					.First();
			}
			catch (VkApiException e)
			{
				throw new IncorrectGroupException(GROUP_ID, e);
			}

			var wallPostParams = new WallPostParams
			{
				// Для группы идентификатор сохраняется со знаком '-'
				OwnerId = -group.Id,
				Message = postText
			};

			try
			{
				VkApi.Wall.Post(wallPostParams);
			}
			catch (VkApiException e)
			{
				throw new PostSendingException(group.Id.ToString(), e);
			}
		}
	}
}