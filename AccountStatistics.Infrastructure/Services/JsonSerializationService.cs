using AccountStatistics.Infrastructure.Services.Interfaces;
using Newtonsoft.Json;

namespace AccountStatistics.Infrastructure.Services
{
	/// <summary>
	/// Сервис для сериализации данных (JSON)
	/// </summary>
	public class JsonSerializationService : ISerializationService
	{
		public string SerializeData(object data)
		{
			return JsonConvert.SerializeObject(data);
		}
	}
}