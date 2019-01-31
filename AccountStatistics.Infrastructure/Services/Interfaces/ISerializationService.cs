namespace AccountStatistics.Infrastructure.Services.Interfaces
{
	/// <summary>
	/// Сервис для сериализации данных
	/// </summary>
	public interface ISerializationService
	{
		/// <summary>
		/// Сериализовать данные
		/// </summary>
		string SerializeData(object data);
	}
}