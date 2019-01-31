namespace AccountStatistics.Infrastructure.Domains
{
	/// <summary>
	/// Пост в социальной сети
	/// </summary>
	public class PostDomain
	{
		public PostDomain(string authorId, string authorName, string text)
		{
			AuthorId = authorId;
			AuthorName = authorName;
			Text = text;
		}

		/// <summary>
		/// Идентификатор автора поста
		/// </summary>
		public string AuthorId { get; }

		/// <summary>
		/// Имя автора поста (если это группа, то ее название)
		/// </summary>
		public string AuthorName { get; }

		/// <summary>
		/// Текст поста
		/// </summary>
		public string Text { get; }

		public override string ToString() => Text;
	}
}