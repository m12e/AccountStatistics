using AccountStatistics.Console.Services;
using AccountStatistics.Console.Services.Interfaces;
using AccountStatistics.Infrastructure.Modules;
using Ninject;
using NLog;
using System;

namespace AccountStatistics.Console
{
	public static class AccountStatistics
	{
		public static IConsoleService GetConsoleService()
		{
			using (var container = new StandardKernel())
			{
				container.Load(new AccountStatisticsInfrastructureModule());
				container.Bind<IConsoleService>().To<ConsoleService>();

				return container.Get<IConsoleService>();
			}
		}

		public static void Main(string[] args)
		{
			var consoleService = GetConsoleService();
			var logger = LogManager.GetCurrentClassLogger();

			try
			{
				consoleService.Run();
			}
			catch (Exception e)
			{
				logger.Error(e);
				consoleService.PrintErrorMessage(e);
			}
		}
	}
}