using AccountStatistics.Infrastructure.Services;
using AccountStatistics.Infrastructure.Services.Interfaces;
using Ninject.Modules;

namespace AccountStatistics.Infrastructure.Modules
{
	public class AccountStatisticsInfrastructureModule : NinjectModule
	{
		public override void Load()
		{
			Bind<ILetterFrequencyService>().To<LetterFrequencyService>();
			Bind<ISerializationService>().To<JsonSerializationService>();
			Bind<ISocialNetworkService>().To<VkSocialNetworkService>();
		}
	}
}